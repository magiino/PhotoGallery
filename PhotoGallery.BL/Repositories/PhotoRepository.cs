using System.Collections.Generic;
using System.Linq;
using PhotoGallery.DAL;
using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.Repositories
{
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
                return mapper.Map(photo);
            }
        }

        public ICollection< PhotoListModel> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return mapper.MapList(dataContext.Photos.ToList());
            }
        }

        public PhotoDetailModel GetById(int id)

        {
            using (var dataContext = new DataContext())
            {
                var photo = dataContext
                 .Photos
                 .FirstOrDefault(r => r.Id == id);
                return mapper.Map(photo);
            }
        }
    }
}