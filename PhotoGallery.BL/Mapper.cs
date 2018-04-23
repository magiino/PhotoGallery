using System.Collections.Generic;
using System.Linq;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL
{
    public static class Mapper
    {
        #region ItemTag
        public static ItemTagModel ItemTagEntityToItemTagListModel(ItemTagEntity itemTagEntity)
        {
            return new ItemTagModel
            {
                Id = itemTagEntity.Id,
                Name = itemTagEntity.Item.Name,
                YPosition = itemTagEntity.YPosition,
                XPosition = itemTagEntity.XPosition
            };
        }

        public static ICollection<ItemTagModel> ItemTagEntitiesToItemTagListModels(IEnumerable<ItemTagEntity> itemTagEntities)
        {
            return itemTagEntities.Select(ItemTagEntityToItemTagListModel).ToList();
        }
        #endregion

        #region Item
        public static ItemModel ItemEntityToItemModel(ItemEntity item)
        {
            return new ItemModel
            {
                Id = item.Id,
                Name = item.Name,
            };
        }

        public static ICollection<ItemModel> ItemEntitiesToItemModel(IEnumerable<ItemEntity> itemEntities)
        {
            return itemEntities.Select(ItemEntityToItemModel).ToList();
        }
        #endregion

        #region PersonTag
        public static PersonTagModel PersonTagEntityToPersonTagListModel(PersonTagEntity personTag)
        {
            return new PersonTagModel
            {
                Id = personTag.Id,
                FirstName = personTag.Person.FirstName,
                LastName = personTag.Person.LastName,

            };
        }

        public static ICollection<PersonTagModel> PersonTagEntitiesToPersonTagListModels(IEnumerable<PersonTagEntity> personTagEntities)
        {
            return personTagEntities.Select(PersonTagEntityToPersonTagListModel).ToList();
        }
        #endregion

        #region Person
        public static PersonModel PersonEntityToPersonModel(PersonEntity person)
        {
            return new PersonModel
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
            };
        }

        public static ICollection<PersonModel> PersonEntitiesToPersonModels(IEnumerable<PersonEntity> personEntities)
        {
            return personEntities.Select(PersonEntityToPersonModel).ToList();
        }
        #endregion

        #region Album
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
        public static ICollection<AlbumModel> AlbumEntitiesToAlbumModels(IEnumerable<AlbumEntity> albumEntities)
        {
            return albumEntities.Select(AlbumEntityToAlbumModel).ToList();
        }

        public static AlbumEntity AlbumModelToAlbumEntity(AlbumModel albumModel)
        {
            return new AlbumEntity()
            {
                Title = albumModel.Title,
            };
        }
        #endregion

        #region Photo
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
        #endregion
    }
}