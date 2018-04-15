using System.Collections.Generic;
using System.Linq;
using PhotoGallery.DAL;
using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.Repositories
{
    public class AlbumRepository 
    {
        private readonly DataContext _dataDontext;

        public AlbumRepository(DataContext dataDontext)
        {
            _dataDontext = dataDontext;
        }

        public AlbumModel FindByTitle(string title)

        {
            using (var dataContext = new DataContext())
            {
                var album = dataContext
                .Albums
                .FirstOrDefault(r => r.Title == title);
                return Mapper.AlbumEntityToAlbumModel(album);
            }
        }

        public ICollection<AlbumModel> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return Mapper.AlbumEntitiesToAlbumModels(dataContext.Albums.ToList());
            }
        }

        public AlbumModel GetById(int id)
        {
            using (var dataContext = new DataContext())
            {
                var album = dataContext
                 .Albums
                 .FirstOrDefault(r => r.Id == id);
                return Mapper.AlbumEntityToAlbumModel(album);
            }
        }
    }
}