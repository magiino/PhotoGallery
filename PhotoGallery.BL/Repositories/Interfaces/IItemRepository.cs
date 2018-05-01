using System.Collections.Generic;
using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IItemRepository
    {
        ItemModel GetById(int id);
        ICollection<ItemModel> GetAll();
        bool Delete(int id);
    }
}