using PhotoGallery.DAL.Entities;
using PhotoGallery.DAL.Repositories.Base;

namespace PhotoGallery.DAL.Repositories
{
    public class AlbumRepository : Repository<AlbumEntity>, IAlbumRepository
    { 
        public AlbumRepository(DataContext dataDontext) : base(dataDontext) { }
    }
}
