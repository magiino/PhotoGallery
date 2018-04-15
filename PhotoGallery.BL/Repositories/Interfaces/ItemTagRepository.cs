using System.Collections.Generic;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IItemTagRepository
    {
        ICollection<ItemTagModel> GetAll();
        ItemTagModel GetByName(string name);

        ItemTagModel GetById(int id);
        ItemTagModel Add(ItemTagEntity item);
        ItemTagModel Delete(int id);
        ItemTagModel Update();
    }
}