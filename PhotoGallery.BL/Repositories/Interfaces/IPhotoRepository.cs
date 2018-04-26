using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PhotoGallery.BL.MessengerFile.Messeges;
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

        PhotoListModel Add(PhotoDetailModel photoDetailModel);
        bool Delete(int id);
        bool Update(PhotoDetailModel photoDetail);

        ICollection<PhotoListModel> GetPhotosByItemTag(ItemTagModel itemTagModel, int pageIndex, int pageSize = IoC.IoC.PageSize);
        ICollection<PhotoListModel> GetPhotosByPersonTag(PersonTagModel personTagModel, int pageIndex, int pageSize = IoC.IoC.PageSize);
        int GetNumberOfPhotosWithThisTag(int id);

        ICollection<PhotoListModel> GetPhotosByPage(int pageIndex, int pageSize = IoC.IoC.PageSize);
        ICollection<PhotoListModel> GetPhotosByPageFilter(Expression<Func<PhotoEntity, bool>> filter, int pageIndex, int pageSize = IoC.IoC.PageSize);
        List<int> GetSortedFilteredPhotosIds(FilterSortSettings settings, ChosenItem item);
    }
}