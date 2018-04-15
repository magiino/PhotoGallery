using System.Collections.Generic;
using System.Linq;
using PhotoGallery.DAL;
using PhotoGallery.BL.Models;
using PhotoGallery.BL.Repositories.Interfaces;

namespace PhotoGallery.BL.Repositories
{
    public class ItemTagRepository
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

    }
}