using System.Collections.Generic;
using System.Linq;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL
{
    public static class Mapper
    {
        #region ItemTag
        public static ItemTagModel ItemTagEntityToItemTagModel(ItemTagEntity itemTagEntity)
        {
            return new ItemTagModel
            {
                Id = itemTagEntity.Id,
                Name = itemTagEntity.Item.Name,
                ItemId = itemTagEntity.ItemId,
                YPosition = itemTagEntity.YPosition,
                XPosition = itemTagEntity.XPosition
            };
        }

        public static ICollection<ItemTagModel> ItemTagEntitiesToItemTagModels(IEnumerable<ItemTagEntity> itemTagEntities)
        {
            return itemTagEntities.Select(ItemTagEntityToItemTagModel).ToList();
        }
        public static ItemTagEntity ItemTagModelToItemTagEntity(ItemTagModel itemTagModel)
        {
            return new ItemTagEntity
            {
                Id = itemTagModel.Id,
                ItemId = itemTagModel.ItemId,
                XPosition = itemTagModel.XPosition,
                YPosition = itemTagModel.YPosition,
            };
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
        public static PersonTagModel PersonTagEntityToPersonTagModel(PersonTagEntity personTagEntity)
        {
            return new PersonTagModel
            {
                Id = personTagEntity.Id,
                PersonId = personTagEntity.PersonId,
                FirstName = personTagEntity.Person.FirstName,
                LastName = personTagEntity.Person.LastName,
            };
        }

        public static ICollection<PersonTagModel> PersonTagEntitiesToPersonTagModels(IEnumerable<PersonTagEntity> personTagEntities)
        {
            return personTagEntities.Select(PersonTagEntityToPersonTagModel).ToList();
        }

        public static PersonTagEntity PersonTagModelToPersonTagEntity(PersonTagModel personTagModel)
        {
            return new PersonTagEntity
            {
                Id = personTagModel.Id,
                PersonId = personTagModel.PersonId,
                XPosition = personTagModel.XPosition,
                YPosition = personTagModel.YPosition,
            };
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

        #region Tag

        public static List<TagModel> TagEntitiesToTagModels(ICollection<TagEntity> tagEntities)
        {
            var tags = new List<TagModel>();
            foreach (var tag in tagEntities)
            {
                switch (tag)
                {
                    case PersonTagEntity personTag:
                        tags.Add(new TagModel()
                        {
                            Id = personTag.Id,
                            PersonId = personTag.PersonId,
                            Name = personTag.Person.FirstName + " " + personTag.Person.LastName,
                            XPosition = personTag.XPosition,
                            YPosition = personTag.YPosition
                        });
                        break;
                    case ItemTagEntity itemTag:
                        tags.Add(new TagModel()
                        {
                            IsItem = true,
                            Id = itemTag.Id,
                            ItemId = itemTag.ItemId,
                            Name = itemTag.Item.Name,
                            XPosition = itemTag.XPosition,
                            YPosition = itemTag.YPosition
                        });
                        break;
                }
            }

            return tags;
        }

        public static List<TagEntity> TagModelsToTagEntities(ICollection<TagModel> tagModels)
        {
            var tags = new List<TagEntity>();
            foreach (var tag in tagModels)
            {
                if(tag.ItemId == 0)
                    tags.Add(new PersonTagEntity()
                    {
                        Id = tag.Id,
                        PersonId = tag.PersonId,
                        XPosition = tag.XPosition,
                        YPosition = tag.YPosition,
                    });
                else tags.Add(new ItemTagEntity()
                    {
                        Id = tag.Id,
                        ItemId = tag.ItemId,
                        XPosition = tag.XPosition,
                        YPosition = tag.YPosition,
                    });
            }

            return tags;
        }

        #endregion

        #region Album
            public static AlbumModel AlbumEntityToAlbumModel(AlbumEntity albumEntity)
        {
            return new AlbumModel
            {
                Id = albumEntity.Id,
                Title = albumEntity.Title,
                CoverPhotoId = albumEntity.CoverPhotoId ?? -1,
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
            var model = new PhotoDetailModel
            {
                Id = photoEntity.Id,
                Name = photoEntity.Name,
                Path = photoEntity.Path,
                CreatedTime = photoEntity.CreatedTime,
                Format = photoEntity.Format,
                AlbumId = photoEntity.AlbumId,
                Resolution = new ResolutionModel()
                {
                    Height = photoEntity.Resolution.Height,
                    Width = photoEntity.Resolution.Width,
                    Id = photoEntity.Resolution.Id
                },
                Note = photoEntity.Note,
                Location = photoEntity.Location,
            };
            if (photoEntity.Tags == null) return model;
            var tags = TagEntitiesToTagModels(photoEntity.Tags);
            model.Tags = tags;

            return model;
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
                CreatedTime = photoEntity.CreatedTime
            };
        }

        public static ICollection<PhotoListModel> PhotoEntitiesToPhotoListModels(IEnumerable<PhotoEntity> entities)
        {
            return entities.Select(PhotoEntityToPhotoListModel).ToList();
        }

        public static PhotoEntity PhotoDetailModelToPhotoEntity(PhotoDetailModel photoDetailModel)
        {
            return new PhotoEntity()
            {
                Id = photoDetailModel.Id,
                Name = photoDetailModel.Name,
                AlbumId = photoDetailModel.AlbumId,
                Format = photoDetailModel.Format,
                Note = photoDetailModel.Note,
                Path = photoDetailModel.Path,
                Location = photoDetailModel.Location,
                ResolutionId = photoDetailModel.Resolution.Id,
                CreatedTime = photoDetailModel.CreatedTime,
            };
        }

        #endregion

        #region Resolution
        public static ResolutionModel ResolutionEntityToResolutionModel(ResolutionEntity resolutionEntity)
        {
            return new ResolutionModel()
            {
                Id = resolutionEntity.Id,
                Width = resolutionEntity.Width,
                Height = resolutionEntity.Height
            };
        }

        public static ICollection<ResolutionModel> ResolutionEntitiesToResolutionModels(IEnumerable<ResolutionEntity> resolutionEntities)
        {
            return resolutionEntities.Select(ResolutionEntityToResolutionModel).ToList();
        }
        public static ResolutionEntity ResolutionModelToResolutionEntity(ResolutionModel resolutionModel)
        {
            return new ResolutionEntity()
            {
                Id = resolutionModel.Id,
                Width = resolutionModel.Width,
                Height = resolutionModel.Height
            };
        }
        #endregion
    }
}