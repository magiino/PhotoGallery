using System.Collections.Generic;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IItemTagRepository
    {
        ItemTagDetailModel GetById(int id);
        ICollection<ItemTagDetailModel> GetAll();
        ItemTagDetailModel GetByName(string name);
        
        ItemTagEntity Add(ItemTagEntity item);
        bool Delete(int id);
        bool Update(ItemTagDetailModel item);
    }
}