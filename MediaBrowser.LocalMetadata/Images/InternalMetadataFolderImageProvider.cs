using System.Collections.Generic;
using System.IO;
using System.Linq;
using MediaBrowser.Controller.Configuration;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Entities.AudioEntity;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.IO;
using Microsoft.Extensions.Logging;

namespace MediaBrowser.LocalMetadata.Images
{
    /// <summary>
    /// Internal metadata folder image provider.
    /// </summary>
    public class InternalMetadataFolderImageProvider : ILocalImageProvider, IHasOrder
    {
        private readonly IServerConfigurationManager _config;
        private readonly IFileSystem _fileSystem;
        private readonly ILogger<InternalMetadataFolderImageProvider> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalMetadataFolderImageProvider"/> class.
        /// </summary>
        /// <param name="config">Instance of the <see cref="IServerConfigurationManager"/> interface.</param>
        /// <param name="fileSystem">Instance of the <see cref="IFileSystem"/> interface.</param>
        /// <param name="logger">Instance of the <see cref="ILogger{InternalMetadataFolderImageProvider}"/> interface.</param>
        public InternalMetadataFolderImageProvider(
            IServerConfigurationManager config,
            IFileSystem fileSystem,
            ILogger<InternalMetadataFolderImageProvider> logger)
        {
            _config = config;
            _fileSystem = fileSystem;
            _logger = logger;
        }

        /// Make sure this is last so that all other locations are scanned first
        /// <inheritdoc />
        public int Order => 1000;

        /// <inheritdoc />
        public string Name => "Internal Images";

        /// <inheritdoc />
        public bool Supports(BaseItem item)
        {
            if (item is Photo)
            {
                return false;
            }

            if (!item.IsSaveLocalMetadataEnabled())
            {
                return true;
            }

            // Extracted images will be saved in here
            if (item is CommonAudioEntity)
            {
                return true;
            }

            if (item.SupportsLocalMetadata && !item.AlwaysScanInternalMetadataPath)
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        public IEnumerable<LocalImageInfo> GetImages(BaseItem item, IDirectoryService directoryService)
        {
            var path = item.GetInternalMetadataPath();

            if (!Directory.Exists(path))
            {
                return Enumerable.Empty<LocalImageInfo>();
            }

            try
            {
                return new LocalImageProvider(_fileSystem).GetImages(item, path, directoryService);
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "Error while getting images for {Library}", item.Name);
                return Enumerable.Empty<LocalImageInfo>();
            }
        }
    }
}
