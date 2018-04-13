using System.Collections.Generic;
using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IPhotoRepository
    {
        PhotoDetailModel FindByName(string name);
        ICollection<PhotoListModel> GetAll();
        PhotoDetailModel GetById(int id);
    }
}