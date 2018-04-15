using PhotoGallery.BL.Repositories;
using PhotoGallery.BL.Repositories.Interfaces;
using PhotoGallery.DAL;

namespace PhotoGallery.BL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;

        public IAlbumRepository Albums { get; }
        public IPhotoRepository Photos { get; }
        public IItemTagRepository ItemTags { get; }
        public IPersonTagRepository PersonTags { get; }

        public UnitOfWork(DataContext context)
        {
            _dataContext = context;

            Albums = new AlbumRepository(_dataContext);
            Photos = new PhotoRepository(_dataContext);
            ItemTags = new ItemTagRepository(_dataContext);
            PersonTags = new PersonTagRepository(_dataContext);
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