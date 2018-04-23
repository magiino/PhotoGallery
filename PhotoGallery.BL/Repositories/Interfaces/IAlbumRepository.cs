using System.Collections.Generic;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IAlbumRepository
    {
        AlbumModel GetById(int id);
        ICollection<AlbumModel> GetAll();
        AlbumModel GetByTitle(string title);

        AlbumModel Add(AlbumModel album);
        bool Delete(int id);
        bool Update(AlbumModel album);
    }
}