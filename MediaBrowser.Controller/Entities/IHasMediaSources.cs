#nullable disable

#pragma warning disable CS1591

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MediaBrowser.Model.Dto;
using MediaBrowser.Model.Entities;

namespace MediaBrowser.Controller.Entities
{
    public interface IHasMediaSources
    {
        Guid Id { get; set; }

        long? RunTimeTicks { get; set; }

        string Path { get; }

        /// <summary>
        /// Gets the media sources.
        /// </summary>
        /// <param name="enablePathSubstitution">Enable path following.</param>
        /// <returns>returns a collection.</returns>
        Collection<MediaSourceInfo> GetMediaSources(bool enablePathSubstitution);

        Collection<MediaStream> GetMediaStreams();
    }
}
