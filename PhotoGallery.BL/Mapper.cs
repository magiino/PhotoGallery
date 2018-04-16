using System.Collections.Generic;
using System.Linq;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL
{
    public static class Mapper
    {
        public static ItemTagListModel ItemTagEntityToItemTagListModel(ItemTagEntity itemTagEntity)
        {
            return new ItemTagListModel
            {
                Id = itemTagEntity.Id,
                Name = itemTagEntity.Name,
            };
        }

        public static  ICollection<ItemTagListModel> ItemTagEntitiesToItemTagListModels(IEnumerable<ItemTagEntity> itemTagEntities)
        {
            return itemTagEntities.Select(ItemTagEntityToItemTagListModel).ToList();
        }

        public static PersonTagListModel PersonTagEntityToPersonTagListModel(PersonTagEntity personTag)
        {
            return new PersonTagListModel
            {
                Id = personTag.Id,
                FirstName = personTag.Person.FirstName,
                LastName = personTag.Person.LastName,
            };
        }

        public static ICollection<PersonTagListModel> PersonTagEntitiesToPersonTagListModels(IEnumerable<PersonTagEntity> personTagEntities)
        {
            return personTagEntities.Select(PersonTagEntityToPersonTagListModel).ToList();
        }

        public static AlbumModel AlbumEntityToAlbumModel(AlbumEntity albumEntity)
        {
            return new AlbumModel
            {
                Id = albumEntity.Id,
                Title = albumEntity.Title,
                CoverPhotoPath = albumEntity.CoverPhoto.Path,
                NumberOfPhotos = albumEntity.Photos.Count
            };
        }
        public static  ICollection<AlbumModel> AlbumEntitiesToAlbumModels(IEnumerable<AlbumEntity> albumEntities)
        {
            return albumEntities.Select(AlbumEntityToAlbumModel).ToList();
        }

        public static PhotoDetailModel PhotoEntityToPhotoDetailModel(PhotoEntity photoEntity)
        {
            return new PhotoDetailModel
            {
                Id = photoEntity.Id,
                Name = photoEntity.Name,
                Path = photoEntity.Path,
                CreatedTime = photoEntity.CreatedTime,
                Format = photoEntity.Format,
                Resolution = new ResolutionModel()
                {
                    Height = photoEntity.Resolution.Height,
                    Width = photoEntity.Resolution.Width,
                    Id = photoEntity.Resolution.Id
                },
                Note = photoEntity.Note,
                Location = photoEntity.Location,
                Tags = photoEntity.Tags
            };
        }

        public static ICollection<PhotoDetailModel> PhotoEntitiesToPhotoDetailModels(IEnumerable<PhotoEntity> photoEntities)
        {
            return photoEntities.Select(PhotoEntityToPhotoDetailModel).ToList();
        }
        
        public static PhotoListModel PhotoEntityToPhotoListModel(PhotoEntity photoEntity)
        {
            return new PhotoListModel
            {
                Id = photoEntity.Id,
                Name = photoEntity.Name,
                Path = photoEntity.Path,
            };
        }

        public static ICollection<PhotoListModel> PhotoEntitiesToPhotoListModels(IEnumerable<PhotoEntity> entities)
        {
            return entities.Select(PhotoEntityToPhotoListModel).ToList();
        }
    }
}