using PhotoGallery.BL.Repositories.Interfaces;
using PhotoGallery.DAL;

namespace PhotoGallery.BL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;
        public IAlbumRepository Albums => throw new System.NotImplementedException();
        public IPhotoRepository Photos => throw new System.NotImplementedException();
        public IItemTagRepository ItemTags => throw new System.NotImplementedException();
        public IPersonTagRepository PersonTags => throw new System.NotImplementedException();

        public UnitOfWork(DataContext context)
        {
            _dataContext = context;

        }

        public int Complete()
        {
            return _dataContext.SaveChanges();
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}