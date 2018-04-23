using PhotoGallery.BL.Repositories.Interfaces;
using System;
using PhotoGallery.BL.Repositories;

namespace PhotoGallery.BL
{
    public interface IUnitOfWork : IDisposable
    {
        IAlbumRepository Albums { get; }
        IPhotoRepository Photos { get; }
        IItemTagRepository ItemTags { get; }
        IPersonTagRepository PersonTags { get; }
        // TODO dorobit interface
        PersonRepository Persons { get; }
        ItemRepository Items { get; }
        ResolutionRepository Resolutions { get; }

        int Complete();
    }
}