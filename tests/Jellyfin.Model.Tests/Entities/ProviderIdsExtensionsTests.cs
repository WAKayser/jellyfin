using System;
using System.Collections.Generic;
using MediaBrowser.Model.Entities;
using Xunit;

namespace Jellyfin.Model.Tests.Entities
{
    public class ProviderIdsExtensionsTests
    {
        private const string ExampleImdbId = "tt0113375";

        [Fact]
        public void HasProviderId_NullInstance_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ProviderIdsExtensions.HasProviderId(null!, MetadataProvider.Imdb));
        }

        [Fact]
        public void HasProviderId_NullProvider_False()
        {
            var nullProvider = new ProviderIdsExtensionsTestsObject
            { };

            Assert.False(nullProvider.HasProviderId(MetadataProvider.Imdb));
        }

        [Fact]
        public void HasProviderId_NullName_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ProviderIdsExtensionsTestsObject.Empty.HasProviderId(null!));
        }

        [Fact]
        public void HasProviderId_NotFoundName_False()
        {
            Assert.False(ProviderIdsExtensionsTestsObject.Empty.HasProviderId(MetadataProvider.Imdb));
        }

        [Fact]
        public void HasProviderId_FoundName_True()
        {
            var provider = new ProviderIdsExtensionsTestsObject();
            provider.SetProviderIdValue(MetadataProvider.Imdb.ToString(), ExampleImdbId);

            Assert.True(provider.HasProviderId(MetadataProvider.Imdb));
        }

        [Fact]
        public void HasProviderId_FoundNameEmptyValue_False()
        {
            var provider = new ProviderIdsExtensionsTestsObject();
            provider.SetProviderIdValue(MetadataProvider.Imdb.ToString(), string.Empty);

            Assert.False(provider.HasProviderId(MetadataProvider.Imdb));
        }

        [Fact]
        public void GetProviderId_NullInstance_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ProviderIdsExtensions.GetProviderId(null!, MetadataProvider.Imdb));
        }

        [Fact]
        public void GetProviderId_NullName_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ProviderIdsExtensionsTestsObject.Empty.GetProviderId(null!));
        }

        [Fact]
        public void GetProviderId_NotFoundName_Null()
        {
            Assert.Null(ProviderIdsExtensionsTestsObject.Empty.GetProviderId(MetadataProvider.Imdb));
        }

        [Fact]
        public void GetProviderId_NullProvider_Null()
        {
            var nullProvider = new ProviderIdsExtensionsTestsObject
            {
            };

            Assert.Null(nullProvider.GetProviderId(MetadataProvider.Imdb));
        }

        [Fact]
        public void TryGetProviderId_NotFoundName_False()
        {
            Assert.False(ProviderIdsExtensionsTestsObject.Empty.TryGetProviderId(MetadataProvider.Imdb, out _));
        }

        [Fact]
        public void TryGetProviderId_NullProvider_False()
        {
            var nullProvider = new ProviderIdsExtensionsTestsObject
            {
            };

            Assert.False(nullProvider.TryGetProviderId(MetadataProvider.Imdb, out _));
        }

        [Fact]
        public void GetProviderId_FoundName_Id()
        {
            var provider = new ProviderIdsExtensionsTestsObject();
            provider.SetProviderIdValue(MetadataProvider.Imdb.ToString(), ExampleImdbId);

            Assert.Equal(ExampleImdbId, provider.GetProviderId(MetadataProvider.Imdb));
        }

        [Fact]
        public void TryGetProviderId_FoundName_True()
        {
            var provider = new ProviderIdsExtensionsTestsObject();
            provider.SetProviderIdValue(MetadataProvider.Imdb.ToString(), ExampleImdbId);

            Assert.True(provider.TryGetProviderId(MetadataProvider.Imdb, out var id));
            Assert.Equal(ExampleImdbId, id);
        }

        [Fact]
        public void TryGetProviderId_FoundNameEmptyValue_False()
        {
            var provider = new ProviderIdsExtensionsTestsObject();
            provider.SetProviderIdValue(MetadataProvider.Imdb.ToString(), string.Empty);

            Assert.False(provider.TryGetProviderId(MetadataProvider.Imdb, out var id));
            Assert.Null(id);
        }

        [Fact]
        public void SetProviderId_NullInstance_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ProviderIdsExtensions.SetProviderId(null!, MetadataProvider.Imdb, ExampleImdbId));
        }

        [Fact]
        public void SetProviderId_Null_Remove()
        {
            var provider = new ProviderIdsExtensionsTestsObject();
            provider.SetProviderId(MetadataProvider.Imdb, null!);
            Assert.Empty(provider.GetProviderId());
        }

        [Fact]
        public void SetProviderId_EmptyName_Remove()
        {
            var provider = new ProviderIdsExtensionsTestsObject();
            provider.SetProviderIdValue(MetadataProvider.Imdb.ToString(), ExampleImdbId);
            provider.SetProviderId(MetadataProvider.Imdb, string.Empty);
            Assert.Empty(provider.GetProviderId());
        }

        [Fact]
        public void SetProviderId_NonEmptyId_Success()
        {
            var provider = new ProviderIdsExtensionsTestsObject();
            provider.SetProviderId(MetadataProvider.Imdb, ExampleImdbId);
            Assert.Single(provider.GetProviderId());
        }

        [Fact]
        public void SetProviderId_NullProvider_Success()
        {
            var nullProvider = new ProviderIdsExtensionsTestsObject
            {
            };

            nullProvider.SetProviderId(MetadataProvider.Imdb, ExampleImdbId);
            Assert.Single(nullProvider.GetProviderId());
        }

        [Fact]
        public void SetProviderId_NullProviderAndEmptyName_Success()
        {
            var nullProvider = new ProviderIdsExtensionsTestsObject
            {
            };

            nullProvider.SetProviderId(MetadataProvider.Imdb, string.Empty);
            Assert.Null(nullProvider.GetProviderId());
        }

        private class ProviderIdsExtensionsTestsObject : IHasProviderIds
        {
            public static readonly ProviderIdsExtensionsTestsObject Empty = new ProviderIdsExtensionsTestsObject();
            private Dictionary<string, string>? _providerIds;

            /// <summary>
            /// Gets or sets the provider ids.
            /// </summary>
            /// <value>The provider ids.</value>
            /// <param name="providerIds">Set the ID.</param>
            public void SetProviderId(Dictionary<string, string> providerIds)
            {
                _providerIds = providerIds;
            }

            public void SetProviderIdValue(string name, string value)
            {
                // If it's null remove the key from the dictionary
                if (string.IsNullOrEmpty(value))
                {
                    _providerIds!.Remove(name);
                }
                else
                {
                    // Ensure it exists
                    _providerIds ??= new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                    _providerIds[name] = value;
                }
            }

            public Dictionary<string, string> GetProviderId() => _providerIds!;
        }
    }
}
