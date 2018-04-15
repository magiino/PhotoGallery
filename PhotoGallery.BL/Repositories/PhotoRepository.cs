using System.Collections.Generic;
using System.Linq;
using PhotoGallery.DAL;
using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.Repositories
{
    public class PhotoRepository
    {
        private readonly DataContext _dataContext;
        public PhotoRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public PhotoDetailModel FindByName(string name)
        {
            using (var dataContext = new DataContext())
            {
                var photo = dataContext
                 .Photos
                 .FirstOrDefault(r => r.Name == name);
                return Mapper.PhotoEntityToPhotoDetailModel(photo);
            }
        }

        public ICollection< PhotoListModel> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return Mapper.PhotoEntitiesToPhotoListModels(dataContext.Photos.ToList());
            }
        }

        public PhotoDetailModel GetById(int id)
        {
            using (var dataContext = new DataContext())
            {
                var photo = dataContext
                 .Photos
                 .FirstOrDefault(r => r.Id == id);
                return Mapper.PhotoEntityToPhotoDetailModel(photo);
            }
        }
    }
}