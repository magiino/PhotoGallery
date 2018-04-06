using PhotoGallery.DAL.Repositories;

namespace PhotoGallery.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;

        public UnitOfWork(DataContext context)
        {
            _dataContext = context;
            Albums = new AlbumRepository(_dataContext);
        }

        public IAlbumRepository Albums { get; }

        public int Complete()
        {
            return _dataContext.SaveChanges();
        }

        public void CompleteAsync()
        {
            _dataContext.SaveChangesAsync();
        }

        public bool HasUnsavedChanges()
        {
            return _dataContext.ChangeTracker.HasChanges();
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}
