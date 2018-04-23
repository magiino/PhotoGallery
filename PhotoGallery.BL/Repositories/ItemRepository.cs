using System.Collections.Generic;
using System.Linq;
using PhotoGallery.DAL;
using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.Repositories
{
    public class ItemRepository //: IItemRepository
    {
        private readonly DataContext _dataContext;

        public ItemRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ItemModel GetById(int id)
        {
            var item = _dataContext.Items.FirstOrDefault(x => x.Id == id);
            return Mapper.ItemEntityToItemModel(item);
        }
        public ICollection<ItemModel> GetAll()
        {
            return Mapper.ItemEntitiesToItemModel(_dataContext.Items.ToList());
        }

        public bool Delete(int id)
        {
            var item = _dataContext.Items.FirstOrDefault(x => x.Id == id);
            if (item == null) return false;

            _dataContext.Items.Remove(item);
            _dataContext.SaveChanges();
            return true;
        }
    }
}