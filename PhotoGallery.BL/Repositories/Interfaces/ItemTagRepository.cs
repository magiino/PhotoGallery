using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        ICollection<PhotoListModel> GetPhotosPredicate(Expression<Func<ItemTagEntity, bool>> predicate, int pageIndex, int pageSize = 6);
    }
}