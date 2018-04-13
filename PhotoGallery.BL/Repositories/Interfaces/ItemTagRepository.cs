using System.Collections.Generic;
using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IItemTagRepository
    {
        ICollection<ItemTagModel> GetAll();
        ItemTagModel GetByName(string name);
    }
}