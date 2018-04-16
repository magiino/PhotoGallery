using System.Collections.Generic;
using System.Linq;
using PhotoGallery.DAL;
using PhotoGallery.BL.Models;
using PhotoGallery.BL.Repositories.Interfaces;
using PhotoGallery.DAL.Entities;
using System;
using System.Linq.Expressions;

namespace PhotoGallery.BL.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DataContext _dataContext;
        public PhotoRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public PhotoDetailModel GetDetailModelById(int id)
        {
            var photo = _dataContext.Photos.FirstOrDefault(x => x.Id == id);
            return Mapper.PhotoEntityToPhotoDetailModel(photo);
        }

        public PhotoListModel GetListModelById(int id)
        {
            var photo = _dataContext.Photos.FirstOrDefault(x => x.Id == id);
            return Mapper.PhotoEntityToPhotoListModel(photo);
        }

        public ICollection<PhotoListModel> GetAll()
        {
            return Mapper.PhotoEntitiesToPhotoListModels(_dataContext.Photos.ToList());
        }

        public PhotoDetailModel GetByName(string name)
        {
            var photo = _dataContext.Photos.FirstOrDefault(x => x.Name == name);
            return Mapper.PhotoEntityToPhotoDetailModel(photo);
        }

        // todo premysliet ci to neprerobit na PhotoModely
        public PhotoEntity Add(PhotoEntity photo)
        {
            var addedPhoto = _dataContext.Photos.Add(photo);
            _dataContext.SaveChanges();
            return addedPhoto;
        }

        public bool Delete(int id)
        {
            var photo = _dataContext.Photos.SingleOrDefault(x => x.Id == id);
            if (photo == null) return false;

            _dataContext.Photos.Remove(photo);
            _dataContext.SaveChanges();
            return true;
        }

        public bool Update(PhotoDetailModel photoDetail)
        {
            var photoEntity = _dataContext.Photos.SingleOrDefault(x => x.Id == photoDetail.Id);
            if (photoEntity == null) return false;

            photoEntity.Name = photoDetail.Name;
            photoEntity.Note = photoDetail.Note;
            photoEntity.Location = photoDetail.Location;
            photoEntity.Tags = photoDetail.Tags;
            _dataContext.SaveChanges();
            return true;
        }

        public ICollection<PhotoListModel> GetPhotosByPage(int pageIndex, int pageSize = IoC.IoC.PageSize)
        {
            return Mapper.PhotoEntitiesToPhotoListModels(_dataContext.Photos
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList());
        }


        public ICollection<PhotoListModel> GetPhotosByPageFilter(Expression<Func<PhotoEntity,bool>> filter, int pageIndex, int pageSize = IoC.IoC.PageSize)
        {
            return Mapper.PhotoEntitiesToPhotoListModels(_dataContext.Photos
                .Where(filter)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList());
        }

        public ICollection<PhotoListModel> GetPhotosByPageFilterWithSort(Expression<Func<PhotoEntity,bool>> filter, Expression<Func<PhotoEntity,bool>> sort, int pageIndex, int pageSize = IoC.IoC.PageSize)
        {
            return Mapper.PhotoEntitiesToPhotoListModels(_dataContext.Photos
                .Where(filter)
                .OrderBy(sort)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList());
        }
    }
}