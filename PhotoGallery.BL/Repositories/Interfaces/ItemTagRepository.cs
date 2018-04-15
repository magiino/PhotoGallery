using System.Collections.Generic;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IItemTagRepository
    {
        ICollection<ItemTagListModel> GetAll();
        ItemTagListModel GetByName(string name);
        ItemTagListModel GetById(int id);
        ItemTagEntity Add(ItemTagEntity item);
        void Delete(int id);
        void Update(ItemTagListModel item);
    }
}