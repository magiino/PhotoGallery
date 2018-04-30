using System.Collections.Generic;
using System.Linq;
using PhotoGallery.DAL;
using PhotoGallery.BL.Models;
using PhotoGallery.BL.Repositories.Interfaces;
using PhotoGallery.DAL.Entities;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq.Expressions;
using Ninject.Infrastructure.Language;
using PhotoGallery.BL.MessengerFile.Messeges;
using PhotoGallery.DAL.Enums;

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

        public PhotoListModel Add(PhotoDetailModel photoDetailModel)
        {
            var addedPhoto = _dataContext.Photos.Add(Mapper.PhotoDetailModelToPhotoEntity(photoDetailModel));

            _dataContext.SaveChanges();
            return Mapper.PhotoEntityToPhotoListModel(addedPhoto);
        }

        public bool Delete(int id)
        {
            var photo = _dataContext.Photos.SingleOrDefault(x => x.Id == id);
            if (photo == null) return false;

            // Ak fotka ktoru mazeme ma ako psoledna toto resolution, tak ho zmazeme z databazy
            var photoWithResolution = _dataContext.Photos.FirstOrDefault(x => x.ResolutionId == photo.ResolutionId);
            if (photoWithResolution == null)
            {
                _dataContext.Resolutions.Remove(new ResolutionEntity()
                {
                    Id = photo.ResolutionId,
                    Width = photo.Resolution.Width,
                    Height = photo.Resolution.Height
                });
            }

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
            photoEntity.Tags = Mapper.TagModelsToTagEntities(photoDetail.Tags);
            _dataContext.SaveChanges();
            return true;
        }

        public ICollection<PhotoListModel> GetPhotosByItemTag(ItemTagModel itemTagModel, int pageIndex, int pageSize = IoC.IoC.PageSize)
        {
            return Mapper.PhotoEntitiesToPhotoListModels(_dataContext.Photos
                .Where(x => x.Tags.Contains(Mapper.ItemTagModelToItemTagEntity(itemTagModel)))
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList());
        }

       
        public ICollection<PhotoListModel> GetPhotosByPersonTag(PersonTagModel personTagModel, int pageIndex, int pageSize = IoC.IoC.PageSize)
        {
            return Mapper.PhotoEntitiesToPhotoListModels(_dataContext.Photos
                .Where(x => x.Tags.Contains(Mapper.PersonTagModelToPersonTagEntity(personTagModel)))
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList());
        }

        public int GetNumberOfPhotosWithThisTag(int id)
        {
            var personTag = _dataContext.PersonTags.SingleOrDefault(x => x.Id == id);
            if (personTag == null)
            {
                var itemTag = _dataContext.ItemTags.SingleOrDefault(x => x.Id == id);
                return _dataContext.Photos.Count(x => x.Tags.Contains(itemTag));
            }
            else
                return _dataContext.Photos.Count(x => x.Tags.Contains(personTag));
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

        public List<int> GetSortedFilteredPhotosIds(FilterSortSettings settings, ChosenItem item)
        {
            Expression<Func<PhotoEntity, bool>> filterExpression = x =>
                settings.Format != Format.None ? x.Format == settings.Format : true
                && settings.ResolutionId > 0 ? x.ResolutionId == settings.ResolutionId : true
                && string.IsNullOrEmpty(settings.SearchString) || x.Name.Contains(settings.SearchString)
                && (x.CreatedTime >= settings.DateFrom && x.CreatedTime <= settings.DateTo);

            var photos = _dataContext.Photos.Where(filterExpression).Include(x => x.Tags).ToList();

            var filteredPhotos = new List<PhotoEntity>();

            switch (item.ItemType)
            {
                case ItemType.Album:
                    filteredPhotos.AddRange(photos.Where(x => x.AlbumId == item.Id).ToList());
                    break;
                case ItemType.Person:
                    var personTags = _dataContext.PersonTags.Where(x => x.PersonId == item.Id).ToList();
                    foreach (var tag in personTags)
                        filteredPhotos.AddRange(photos.Where(x => x.Tags.Contains(tag)).ToList());
                    break;
                case ItemType.Item:
                    var itemTags = _dataContext.ItemTags.Where(x => x.Item.Id == item.Id);
                    foreach (var tag in itemTags)
                        filteredPhotos.AddRange(photos.Where(x => x.Tags.Contains(tag)).ToList());
                    break;
                default:
                    Debugger.Break();
                    throw new ArgumentOutOfRangeException();
            }

            switch (settings.Sort)
            {
                case Sort.ByDateTime when settings.SortAscending:
                    return filteredPhotos.OrderBy(x => x.CreatedTime).Select(x => x.Id).ToList();
                case Sort.ByDateTime when !settings.SortAscending:
                    return filteredPhotos.OrderByDescending(x => x.CreatedTime).Select(x => x.Id).ToList();
                case Sort.ByName when settings.SortAscending:
                    return filteredPhotos.OrderBy(x => x.Name).Select(x => x.Id).ToList();
                case Sort.ByName when !settings.SortAscending:
                    return filteredPhotos.OrderByDescending(x => x.Name).Select(x => x.Id).ToList();
                case Sort.None:
                    return filteredPhotos.Select(x => x.Id).ToList();
                default:
                    Debugger.Break();
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}