using System.Collections.Generic;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IItemTagRepository
    {
        ItemTagModel GetById(int id);
        ICollection<ItemTagModel> GetAll();
        ItemTagModel GetByName(string name);
        
        ItemTagEntity Add(ItemTagEntity item);
        bool Delete(int id);
        bool Update(ItemTagModel item);
    }
}