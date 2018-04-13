using System.Collections.Generic;
using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IAlbumRepository
    {
        AlbumModel FindByTitle(string title);
        ICollection<AlbumModel> GetAll();
        AlbumModel GetById(int id);
    }
}