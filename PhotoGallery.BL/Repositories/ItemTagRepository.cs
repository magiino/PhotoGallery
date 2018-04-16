using System.Collections.Generic;
using System.Linq;
using PhotoGallery.DAL;
using PhotoGallery.BL.Models;
using PhotoGallery.BL.Repositories.Interfaces;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL.Repositories
{
    public class ItemTagRepository : IItemTagRepository
    {
        private readonly DataContext _dataContext;

        public ItemTagRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ItemTagListModel GetById(int id)
        {
            var itemTag = _dataContext.ItemTags.FirstOrDefault(x => x.Id == id);
            return Mapper.ItemTagEntityToItemTagListModel(itemTag);
        }
        public ICollection<ItemTagListModel> GetAll()
        {
            return Mapper.ItemTagEntitiesToItemTagListModels(_dataContext.ItemTags.ToList());
        }
        public ItemTagListModel GetByName(string name)
        {
            var itemTag = _dataContext.ItemTags.FirstOrDefault(x => x.Name == name);
            return Mapper.ItemTagEntityToItemTagListModel(itemTag);
        }
 
        public ItemTagEntity Add(ItemTagEntity item)
        {
            var addedItem =_dataContext.ItemTags.Add(item);
            _dataContext.SaveChanges();
            return addedItem;
        }

        public bool Delete(int id)
        {
            var item = _dataContext.ItemTags.FirstOrDefault(r => r.Id == id);
            if (item == null) return false;
            _dataContext.ItemTags.Remove(item);
            _dataContext.SaveChanges();
            return true;
        }

        public bool Update(ItemTagDetailModel item)
        {
            var itemTagEntity = _dataContext.ItemTags.SingleOrDefault(x => x.Id == item.Id);
            if (itemTagEntity == null) return false;

            itemTagEntity.XPosition = item.XPosition;
            itemTagEntity.YPosition = item.YPosition;
            itemTagEntity.Name = item.Name;

            _dataContext.SaveChanges();
            return true;
        }
    }
}