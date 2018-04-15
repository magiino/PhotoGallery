using System.Collections.Generic;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IAlbumRepository
    {
        AlbumModel FindByTitle(string title);

        ICollection<AlbumModel> GetAll();
        AlbumModel GetById(int id);
        AlbumEntity Add(AlbumEntity album);
        void Delete(int id);
        void Update(AlbumModel album);

    }
}