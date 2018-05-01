using System.Collections.Generic;
using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IItemTagRepository
    {
        ItemTagModel GetById(int id);
        ICollection<ItemTagModel> GetAll();
        ICollection<ItemTagModel> GetByItemId(int itemId);

        int Add(TagModel item, PhotoDetailModel photo);
        bool Delete(int id);
    }
}