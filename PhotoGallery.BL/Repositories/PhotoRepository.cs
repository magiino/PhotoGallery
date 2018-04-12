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

    public class PhotoRepository
    {
        private readonly Mapper mapper = new Mapper();

        public PhotoDetailModel FindByName(string name)

        {
            using (var dataContext = new DataContext())
            {
                var photo = dataContext
                 .Photos
                 .FirstOrDefault(r => r.Name == name);
                return this.mapper.Map(photo);
            }

        }

        public ICollection< PhotoListModel> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return this.mapper.MapList(dataContext.Photos.ToList());

            }
                
        }

        public PhotoDetailModel GetById(int id)

        {
            using (var dataContext = new DataContext())
            {
                var photo = dataContext
                 .Photos
                 .FirstOrDefault(r => r.Id == id);
                return this.mapper.Map(photo);
            }

        }

    }
}
