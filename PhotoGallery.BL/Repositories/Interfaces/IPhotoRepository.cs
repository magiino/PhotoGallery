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

        PhotoDetailModel Add(PhotoEntity photo);
        PhotoDetailModel Delete(int id);
        PhotoDetailModel Update();

        ICollection<PhotoListModel> GetWithName(int pageIndex, int pageSize);
        ICollection<PhotoListModel> GetWithTime(int pageIndex, int pageSize);
        ICollection<PhotoListModel> GetWithNamePredicate(Predicate<bool> predicate, int pageIndex, int pageSize);
        ICollection<PhotoListModel> GetWithDatePredicate(Predicate<bool> predicate, int pageIndex, int pageSize);
        ICollection<PhotoListModel> GetWithNamePredicateOrdered(Predicate<bool> predicate, int pageIndex, int pageSize);
        ICollection<PhotoListModel> GetWithDatePredicateOrdered(Predicate<bool> predicate, int pageIndex, int pageSize);
    }
}