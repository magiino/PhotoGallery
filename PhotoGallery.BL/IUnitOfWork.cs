using System;

namespace PhotoGallery.BL
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
    }
}