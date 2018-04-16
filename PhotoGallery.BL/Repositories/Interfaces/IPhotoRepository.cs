using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IPhotoRepository
    {
        PhotoDetailModel GetDetailModelById(int id);
        PhotoListModel GetListModelById(int id);
        ICollection<PhotoListModel> GetAll();
        PhotoDetailModel GetByName(string name);

        PhotoEntity Add(PhotoEntity photo);
        bool Delete(int id);
        bool Update(PhotoDetailModel photoDetail);

        ICollection<PhotoListModel> GetPhotosByPage(int pageIndex, int pageSize = IoC.IoC.PageSize);
        ICollection<PhotoListModel> GetPhotosByPageFilter(Expression<Func<PhotoEntity, bool>> filter, int pageIndex, int pageSize = IoC.IoC.PageSize);
        ICollection<PhotoListModel> GetPhotosByPageFilterWithSort(Expression<Func<PhotoEntity, bool>> filter, Expression<Func<PhotoEntity, bool>> sort, int pageIndex, int pageSize = IoC.IoC.PageSize);
    }
}