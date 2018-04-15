using System.Collections.Generic;
using System.Linq;
using PhotoGallery.DAL;
using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.Repositories
{
    public class ItemTagRepository
    {
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
    }
}