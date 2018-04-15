using System.Collections.Generic;
using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IItemTagRepository
    {
        ICollection<ItemTagListModel> GetAll();
        ItemTagListModel GetByName(string name);
    }
}