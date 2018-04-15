using System;
using System.Collections.Generic;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IPhotoRepository
    {
        PhotoDetailModel FindByName(string name);
        ICollection<PhotoListModel> GetAll();
        PhotoDetailModel GetById(int id);

        PhotoEntity Add(PhotoEntity photo);
        void Delete(int id);
        void Update(PhotoDetailModel photo);

        ICollection<PhotoListModel> GetPhotos(int pageIndex, int pageSize);
        ICollection<PhotoListModel> GetWithNamePredicate(Predicate<bool> predicate, int pageIndex, int pageSize);
        ICollection<PhotoListModel> GetWithDatePredicate(Predicate<bool> predicate, int pageIndex, int pageSize);
        ICollection<PhotoListModel> GetWithNamePredicateOrdered(Predicate<bool> predicate, int pageIndex, int pageSize);
        ICollection<PhotoListModel> GetWithDatePredicateOrdered(Predicate<bool> predicate, int pageIndex, int pageSize);
    }
}