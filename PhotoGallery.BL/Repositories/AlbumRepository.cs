using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using PhotoGallery.DAL;
using PhotoGallery.BL.Models;
using PhotoGallery.BL.Repositories.Interfaces;

namespace PhotoGallery.BL.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly DataContext _dataContext;

        public AlbumRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public AlbumModel GetById(int id)
        {
            var album = _dataContext.Albums.FirstOrDefault(x => x.Id == id);
            return Mapper.AlbumEntityToAlbumModel(album);
        }
        public ICollection<AlbumModel> GetAll()
        {
            var albums = Mapper.AlbumEntitiesToAlbumModels(_dataContext.Albums.ToList());
            foreach (var album in albums)
            {
                if (album.CoverPhotoId > 0)
                    album.CoverPhotoPath = _dataContext.Photos.SingleOrDefault(x => x.Id == album.CoverPhotoId)?.Path;
            }
            return albums;
        }
        public AlbumModel GetByTitle(string title)
        {
            var album = _dataContext.Albums.FirstOrDefault(x => x.Title == title);
            return Mapper.AlbumEntityToAlbumModel(album);
        }

        public AlbumModel Add(AlbumModel album)
        {
            var addedAlbum = _dataContext.Albums.Add(Mapper.AlbumModelToAlbumEntity(album));
            _dataContext.SaveChanges();
            album.Id = addedAlbum.Id;
            return album;
        }

        public bool Delete(int id)
        {
            var album = _dataContext.Albums.Include(x => x.Photos).FirstOrDefault(x => x.Id == id);
            if (album == null) return false;
            _dataContext.Albums.Remove(album);
            _dataContext.SaveChanges();
            return true;
        }

        public bool Update(AlbumModel album)
        {
            var albumEntity = _dataContext.Albums.SingleOrDefault(x => x.Id == album.Id);
            if (albumEntity == null) return false;

            albumEntity.Title = album.Title;
            albumEntity.CoverPhotoId = album.CoverPhotoId;

            _dataContext.SaveChanges();
            return true;
        }

    }
}