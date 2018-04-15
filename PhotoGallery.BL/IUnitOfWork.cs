using System;
using PhotoGallery.BL.Repositories.Interfaces;

namespace PhotoGallery.BL
{
    public interface IUnitOfWork : IDisposable
    {
        IAlbumRepository Albums { get; }
        IPhotoRepository Photos { get; }
        IItemTagRepository ItemTags { get; }
        IPersonTagRepository PersonTags { get; }

        int Complete();
    }
}