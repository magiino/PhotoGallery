using System.Collections.Generic;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL
{
    public class Mapper
    {
        public ItemTagModel Map(ItemTagEntity entity)
        {
            return new ItemTagModel
            {
                Name = entity.Name,
                PositionOnPhotoX = entity.XPosition,
                PositionOnPhotoY = entity.YPosition,
                PhotosWithThisTag = entity.Photos
            };
        }

        public ICollection<ItemTagModel> Map(IEnumerable<ItemTagEntity> entities)
        {
            var models = new List<ItemTagModel>();
            foreach (var entity in entities)
            {
                models.Add(Map(entity));
            }

            return models;
        }

        public PersonTagModel Map(PersonTagEntity entity)
        {
            return new PersonTagModel
            {
                Person = entity.Person,
                PersonId = entity.PersonId,
                PositionOnPhotoX = entity.XPosition,
                PositionOnPhotoY = entity.YPosition,
                PhotosWithThisTag = entity.Photos
            };
        }

        public ICollection<PersonTagModel> Map(IEnumerable<PersonTagEntity> entities)
        {
            var models = new List<PersonTagModel>();
            foreach (var entity in entities)
            {
                models.Add(Map(entity));
            }

            return models;
        }

        public AlbumModel Map(AlbumEntity entity)
        {
            return new AlbumModel
            {
                Id = entity.Id,
                Title = entity.Title,
                CoverPhotoPath = entity.CoverPhoto.Path,
                NumberOfPhotos = entity.Photos.Count
            };
        }
        public ICollection<AlbumModel> Map(IEnumerable<AlbumEntity> entities)
        {
            var models = new List<AlbumModel>();
            foreach (var entity in entities)
            {
                models.Add(Map(entity));
            }
            return models;
        }

        public PhotoDetailModel Map(PhotoEntity entity)
        {
            return new PhotoDetailModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Path = entity.Path,
                CreatedTime = entity.CreatedTime,
                Format = entity.Format,
                Resolution = entity.Resolution,
                Note = entity.Note,
                Location = entity.Location,
                Album = entity.Album,
                Tags = entity.Tags
            };
        }

        public ICollection<PhotoDetailModel> Map(IEnumerable<PhotoEntity> entities)
        {
            var models = new List<PhotoDetailModel>();
            foreach (var entity in entities)
            {
                models.Add(Map(entity));
            }
            return models;
        }
        
        public PhotoListModel MapList(PhotoEntity entity)
        {
            return new PhotoListModel
            {
                Name = entity.Name,
                Path = entity.Path,
            };
        }

        public ICollection<PhotoListModel> MapList(IEnumerable<PhotoEntity> entities)
        {
            var models = new List<PhotoListModel>();
            foreach (var entity in entities)
            {
                models.Add(MapList(entity));
            }
            return models;
        }
    }
}