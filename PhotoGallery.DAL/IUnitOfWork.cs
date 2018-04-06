using PhotoGallery.DAL.Repositories;
using System;

namespace PhotoGallery.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IAlbumRepository Albums { get; }
        int Complete();
        void CompleteAsync();
        bool HasUnsavedChanges();
    }
}
