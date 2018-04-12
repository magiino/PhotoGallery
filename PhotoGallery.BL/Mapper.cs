namespace PhotoGallery.BL

{
    using System.Collections.Generic;
    using PhotoGallery.BL.Models;
    using PhotoGallery.DAL.Entities;
    public class Mapper
    {
        public ItemTagModel Map(ItemTagEntity entity)
        {
            return new ItemTagModel
            {
                Name = entity.Name,
                PositionOnPhotoX = entity.PositionOnPhotoX,
                PositionOnPhotoY = entity.PositionOnPhotoY,
                PhotosWithThisTag = entity.PhotosWithThisTag

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
                PositionOnPhotoX = entity.PositionOnPhotoX,
                PositionOnPhotoY = entity.PositionOnPhotoY,
                PhotosWithThisTag = entity.PhotosWithThisTag
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
            }
            ;
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
                Albums = entity.Albums,
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
                Albums = entity.Albums
            };

        }

        public ICollection<PhotoListModel> MapList(IEnumerable<PhotoEntity> entities)
        {
            var models = new List<PhotoListModel>();
            foreach (var entity in entities)
            {
                models.Add(Map(entity));
            }
            return models;
        }

    }
}
