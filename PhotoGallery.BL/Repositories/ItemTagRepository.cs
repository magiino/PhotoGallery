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
        private readonly DataContext _dataDontext;

        public ItemTagRepository(DataContext dataDontext)
        {
            _dataDontext = dataDontext;
        }

        public ICollection<ItemTagListModel> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return Mapper.ItemTagEntitiesToItemTagListModels(dataContext.ItemTags.ToList());

            }
        }

        public ItemTagListModel GetByName(string name)
        {
            using (var dataContext = new DataContext())
            {
                var itemTag = dataContext
                 .ItemTags
                 .FirstOrDefault(r => r.Name == name);
                return Mapper.ItemTagEntityToItemTagListModel(itemTag);
            }
        }
 
        public ItemTagListModel GetById(int id)
        {
            var itemTag = _dataDontext
                .ItemTags
                .FirstOrDefault(r => r.Id == id);
            return Mapper.ItemTagEntityToItemTagListModel(itemTag);
        }

        public ItemTagEntity Add(ItemTagEntity item)
        {
            _dataDontext.ItemTags.Add(item);
            _dataDontext.SaveChanges();
            return item;
        }

        public void Delete(int id)
        {
            var item = _dataDontext.ItemTags.FirstOrDefault(r => r.Id == id);
            _dataDontext.ItemTags.Remove(item);
        }

        public void Update(ItemTagListModel item)
        {
        }

    }
}