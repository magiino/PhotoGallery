using System.Collections.Generic;
using System.Linq;
using PhotoGallery.DAL;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Entities;

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


        private readonly DataContext dataDontext;

        public AlbumRepository(DataContext dataDontext)
        {
            dataDontext = dataDontext;
        }

        public AlbumEntity Add(AlbumEntity album)
        {
            dataDontext.Albums.Add(album);
            dataDontext.SaveChanges();
            return album;
        }

        public void Delete(int id)
        {
            var album = dataDontext.Albums.FirstOrDefault(r => r.Id == id);
            dataDontext.Albums.Remove(album);
        }

        public void Update(AlbumModel album)
        {
        }

    }
}