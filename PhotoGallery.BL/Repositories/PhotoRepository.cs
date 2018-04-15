using System.Collections.Generic;
using System.Linq;
using PhotoGallery.DAL;
using PhotoGallery.BL.Models;
using PhotoGallery.BL.Repositories.Interfaces;
using PhotoGallery.DAL.Entities;
using System;

namespace PhotoGallery.BL.Repositories
{
    public class PhotoRepository : IPhotoRepository
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

        public PhotoEntity Add(PhotoEntity photo)
        {
            _dataContext.Photos.Add(photo);
            _dataContext.SaveChanges();
            return photo;
        }

        public void Delete(int id)
        {
            var photo = _dataContext.Photos.FirstOrDefault(r => r.Id == id);
            _dataContext.Photos.Remove(photo);
        }

        public void Update(PhotoDetailModel photo)
        {
        }


        public ICollection<PhotoListModel> GetPhotos(int pageIndex, int pageSize)
        {
            return Mapper.PhotoEntitiesToPhotoListModels(_dataContext.Photos
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList());
        }


        public ICollection<PhotoListModel> GetWithNamePredicate(Predicate<bool> predicate, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public ICollection<PhotoListModel> GetWithDatePredicate(Predicate<bool> predicate, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public ICollection<PhotoListModel> GetWithNamePredicateOrdered(Predicate<bool> predicate, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public ICollection<PhotoListModel> GetWithDatePredicateOrdered(Predicate<bool> predicate, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}