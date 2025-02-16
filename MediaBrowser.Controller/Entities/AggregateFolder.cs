#nullable disable

#pragma warning disable CS1591

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using MediaBrowser.Controller.IO;
using MediaBrowser.Controller.Library;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.IO;

namespace MediaBrowser.Controller.Entities
{
    /// <summary>
    /// Specialized folder that can have items added to it's children by external entities.
    /// Used for our RootFolder so plug-ins can add items.
    /// </summary>
    public class AggregateFolder : Folder
    {
        /// <summary>
        /// The _virtual children.
        /// </summary>
        private readonly ConcurrentBag<BaseItem> _virtualChildren = new ConcurrentBag<BaseItem>();

        private readonly object _childIdsLock = new object();
        private bool _requiresRefresh;
        private string[] physicalLocationsList;
        private Guid[] _childrenIds = null;

        public AggregateFolder()
        {
            SetPhysicalLocationsList(Array.Empty<string>());
        }

        [JsonIgnore]
        public override bool IsPhysicalRoot => true;

        [JsonIgnore]
        public override bool SupportsPlayedStatus => false;

        /// <summary>
        /// Gets the virtual children.
        /// </summary>
        /// <value>The virtual children.</value>
        public ConcurrentBag<BaseItem> VirtualChildren => _virtualChildren;

        [JsonIgnore]
        public override string[] PhysicalLocations
        {
            get
            {
                return GetPhysicalLocationsList();
            }
        }

        public string[] GetPhysicalLocationsList()
        {
            return physicalLocationsList;
        }

        public void SetPhysicalLocationsList(string[] value)
        {
            physicalLocationsList = value;
        }

        public override bool CanDelete()
        {
            return false;
        }

        protected override FileSystemMetadata[] GetFileSystemChildren(IDirectoryService directoryService)
        {
            return CreateResolveArgs(directoryService, true).FileSystemChildren;
        }

        protected override List<BaseItem> LoadChildren()
        {
            lock (_childIdsLock)
            {
                if (_childrenIds == null || _childrenIds.Length == 0)
                {
                    var list = base.LoadChildren();
                    _childrenIds = list.Select(i => i.Id).ToArray();
                    return list;
                }

                return _childrenIds.Select(LibraryManager.GetItemById).Where(i => i != null).ToList();
            }
        }

        private void ClearCache()
        {
            lock (_childIdsLock)
            {
                _childrenIds = null;
            }
        }

        public override bool RequiresRefresh()
        {
            var changed = base.RequiresRefresh() || _requiresRefresh;

            if (!changed)
            {
                var locations = PhysicalLocations;

                var newLocations = CreateResolveArgs(new DirectoryService(FileSystem), false).PhysicalLocations;

                if (!locations.SequenceEqual(newLocations))
                {
                    changed = true;
                }
            }

            return changed;
        }

        public override bool BeforeMetadataRefresh(bool replaceAllMetadata)
        {
            ClearCache();

            var changed = base.BeforeMetadataRefresh(replaceAllMetadata) || _requiresRefresh;
            _requiresRefresh = false;
            return changed;
        }

        private ItemResolveArgs CreateResolveArgs(IDirectoryService directoryService, bool setPhysicalLocations)
        {
            ClearCache();

            var path = ContainingFolderPath;

            var args = new ItemResolveArgs(ConfigurationManager.ApplicationPaths, directoryService)
            {
                FileInfo = FileSystem.GetDirectoryInfo(path)
            };

            // Gather child folder and files
            if (args.IsDirectory)
            {
                // When resolving the root, we need it's grandchildren (children of user views)
                var flattenFolderDepth = 2;

                var files = FileData.GetFilteredFileSystemEntries(directoryService, args.Path, FileSystem, CollectionFolder.ApplicationHost, Logger, args, flattenFolderDepth: flattenFolderDepth, resolveShortcuts: true);

                // Need to remove subpaths that may have been resolved from shortcuts
                // Example: if \\server\movies exists, then strip out \\server\movies\action
                files = LibraryManager.NormalizeRootPathList(files).ToArray();

                args.FileSystemChildren = files;
            }

            _requiresRefresh = _requiresRefresh || !args.PhysicalLocations.SequenceEqual(PhysicalLocations);
            if (setPhysicalLocations)
            {
                SetPhysicalLocationsList(args.PhysicalLocations);
            }

            return args;
        }

        protected override IEnumerable<BaseItem> GetNonCachedChildren(IDirectoryService directoryService)
        {
            return base.GetNonCachedChildren(directoryService).Concat(_virtualChildren);
        }

        protected override async Task ValidateChildrenInternal(IProgress<double> progress, bool recursive, bool refreshChildMetadata, MetadataRefreshOptions refreshOptions, IDirectoryService directoryService, CancellationToken cancellationToken)
        {
            ClearCache();

            await base.ValidateChildrenInternal(progress, recursive, refreshChildMetadata, refreshOptions, directoryService, cancellationToken)
                .ConfigureAwait(false);

            ClearCache();
        }

        /// <summary>
        /// Adds the virtual child.
        /// </summary>
        /// <param name="child">The child.</param>
        /// <exception cref="ArgumentNullException"> if child is <c>null</c>. </exception>
        public void AddVirtualChild(BaseItem child)
        {
            if (child == null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            _virtualChildren.Add(child);
        }

        /// <summary>
        /// Finds the virtual child.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>BaseItem.</returns>
        /// <exception cref="ArgumentNullException">The id is empty.</exception>
        public BaseItem FindVirtualChild(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                throw new ArgumentNullException(nameof(id));
            }

            foreach (var child in _virtualChildren)
            {
                if (child.Id == id)
                {
                    return child;
                }
            }

            return null;
        }
    }
}
