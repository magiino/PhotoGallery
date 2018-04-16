using System.Collections.Generic;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IItemTagRepository
    {
        ItemTagListModel GetById(int id);
        ICollection<ItemTagListModel> GetAll();
        ItemTagListModel GetByName(string name);
        
        ItemTagEntity Add(ItemTagEntity item);
        bool Delete(int id);
        bool Update(ItemTagDetailModel item);
    }
}