using System.Collections.Generic;
using System.Linq;
using PhotoGallery.DAL;
using PhotoGallery.BL.Models;
using PhotoGallery.BL.Repositories.Interfaces;

namespace PhotoGallery.BL.Repositories
{
    public class ItemTagRepository
    {
        private readonly Mapper mapper = new Mapper();

        public ICollection<ItemTagModel> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return mapper.Map(dataContext.ItemTags.ToList());

            }
        }

        public ItemTagModel GetByName(string name)
        {
            using (var dataContext = new DataContext())
            {
                var itemTag = dataContext
                 .ItemTags
                 .FirstOrDefault(r => r.Name == name);
                return mapper.Map(itemTag);
            }
        }



        private readonly DataContext dataDontext;

        public ItemTagRepository(DataContext dataDontext)
        {
            dataDontext = dataDontext;
        }

        public ItemTagModel GetById(int id)
        {
            var itemTag = dataDontext
                .ItemTags
                .FirstOrDefault(r => r.Id == id);
            return mapper.Map(itemTag);
        }

    }
}