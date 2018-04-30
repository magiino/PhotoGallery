using System.Collections.Generic;
using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IItemTagRepository
    {
        ItemTagModel GetById(int id);
        ICollection<ItemTagModel> GetAll();
        ItemTagModel GetByName(string name);

        int Add(TagModel item, PhotoDetailModel photo);
        bool Delete(int id);
        bool Update(ItemTagModel item);
    }
}