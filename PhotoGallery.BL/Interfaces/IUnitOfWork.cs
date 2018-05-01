using System;
using PhotoGallery.BL.Repositories.Interfaces;

namespace PhotoGallery.BL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAlbumRepository Albums { get; }
        IPhotoRepository Photos { get; }
        IItemTagRepository ItemTags { get; }
        IPersonTagRepository PersonTags { get; }
        IPersonRepository Persons { get; }
        IItemRepository Items { get; }
        IResolutionRepository Resolutions { get; }

        int Complete();
    }
}