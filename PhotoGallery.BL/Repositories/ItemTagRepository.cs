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

        public ItemTagModel GetById(int id)
        {
            var itemTag = _dataContext.ItemTags.FirstOrDefault(x => x.Id == id);
            return Mapper.ItemTagEntityToItemTagModel(itemTag);
        }

        public ICollection<ItemTagModel> GetAll()
        {
            return Mapper.ItemTagEntitiesToItemTagModels(_dataContext.ItemTags.ToList());
        }
        public ItemTagModel GetByName(string name)
        {
            var itemTag = _dataContext.ItemTags.FirstOrDefault(x => x.Item.Name == name);
            return Mapper.ItemTagEntityToItemTagModel(itemTag);
        }
 
        public int Add(TagModel item, PhotoDetailModel photoDetailModel)
        {
            var itemEntity = _dataContext.Items.FirstOrDefault(x => x.Name == item.Name) ?? _dataContext.Items.Add(new ItemEntity() {Name = item.Name});
            _dataContext.SaveChanges();

            var addedItemTag =_dataContext.ItemTags.Add(new ItemTagEntity()
            {
                Item = itemEntity,
                ItemId = itemEntity.Id,
                XPosition = item.XPosition,
                YPosition = item.YPosition,
                Photos = new List<PhotoEntity>() { Mapper.PhotoDetailModelToPhotoEntity(photoDetailModel) }
            });
            _dataContext.SaveChanges();
            return addedItemTag.Id;
        }

        public bool Delete(int id)
        {
            var item = _dataContext.ItemTags.FirstOrDefault(x => x.Id == id);
            if (item == null) return false;

            var deletePersonEntity = true;
            foreach (var itemTag in _dataContext.ItemTags)
            {
                if (itemTag.ItemId != item.ItemId) continue;
                deletePersonEntity = false;
                break;
            }

            if (deletePersonEntity)
                _dataContext.Items.Remove(item.Item);

            _dataContext.ItemTags.Remove(item);
            _dataContext.SaveChanges();
            return true;
        }

        public bool Update(ItemTagModel item)
        {
            var itemTagEntity = _dataContext.ItemTags.SingleOrDefault(x => x.Id == item.Id);
            if (itemTagEntity == null) return false;

            itemTagEntity.XPosition = item.XPosition;
            itemTagEntity.YPosition = item.YPosition;
            itemTagEntity.Item.Name = item.Name;

            _dataContext.SaveChanges();
            return true;
        }
    }
}