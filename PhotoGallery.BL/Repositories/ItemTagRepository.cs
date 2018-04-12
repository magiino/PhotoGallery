using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.BL.Repositories
{
    using PhotoGallery.DAL;
    using PhotoGallery.BL.Models;
    using PhotoGallery.BL;

    class ItemTagRepository
    {
        private readonly Mapper mapper = new Mapper();

        public ICollection<ItemTagModel> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return this.mapper.Map(dataContext.ItemTags.ToList());

            }

        }

        public ItemTagModel GetByName(string name)
        {
            using (var dataContext = new DataContext())
            {
                var itemTag = dataContext
                 .ItemTags
                 .FirstOrDefault(r => r.Name == name);
                return this.mapper.Map(itemTag);
            }

        }

    }
}
