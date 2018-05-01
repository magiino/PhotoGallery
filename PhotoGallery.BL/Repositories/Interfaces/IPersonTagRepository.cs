using System.Collections.Generic;
using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IPersonTagRepository
    {
        ICollection<PersonTagModel> GetByPersonId(int personId);
        int Add(TagModel item, PhotoDetailModel photo);
        bool Delete(int id);
    }
}