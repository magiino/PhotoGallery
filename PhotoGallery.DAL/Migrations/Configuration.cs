using System.Collections.Generic;
using System.IO;
using System;
using System.Data.Entity.Migrations;
using PhotoGallery.DAL.Entities;
using PhotoGallery.DAL.Enums;

namespace PhotoGallery.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<PhotoGallery.DAL.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        
        protected override void Seed(PhotoGallery.DAL.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.


            var person1 = new PersonEntity()
            {
                Id = 1,
                FirstName = "Jozko",
                LastName = "Mrkvièka",
            };

            var item1 = new ItemEntity()
            {
                Id = 1,
                Name = "Okno"
            };


            var resolution = new ResolutionEntity()
            {
                Id = 1,
                Height = 836,
                Width = 1254,
            };

            var itemTag1 = new ItemTagEntity()
            {
                Id = 1,
                ItemId = 1,
                //Item = item1,
                XPosition = 200,
                YPosition = 350,
            };
            var personTag1 = new PersonTagEntity()
            {
                Id = 2,
                PersonId = 1,
                //Person = person1,
                XPosition = 500,
                YPosition = 700,
            };

            var photo1 = new PhotoEntity()
            {
                Id = 1,
                Name = "Auto",
                Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "fotky\\07a077c43bd4972c0d5ca06fe593_base_optimal.jpg"),
                CreatedTime = DateTime.Now,
                Note = "AutoooooooooooooooooooWooho",
                Location = "Brno",
                Format = Format.Jpg,
                ResolutionId = 1,
                //Resolution = resolution,
                Tags = new List<TagEntity>(),
                AlbumId = 1,
            };

            var album1 = new AlbumEntity()
            {
                Id = 1,
                Title = "Autá",
                CoverPhotoId = 1,
                //CoverPhoto = photo1,
            };
            // TODO opytat sa na prednaske preco vychadzuje chyby pri seede

            //photo1.Tags.Add(personTag1);
            //photo1.Tags.Add(itemTag1);

            //context.Persons.AddOrUpdate(x => x.Id, person1);
            //context.Resolutions.AddOrUpdate(x => x.Id, resolution);

            //context.ItemTags.AddOrUpdate(x => x.Id, itemTag1);
            //context.PersonTags.AddOrUpdate(x => x.Id, personTag1);
            //context.Photos.AddOrUpdate(x => x.Id, photo1);
            //context.Albums.AddOrUpdate(x => x.Id, album1);
        }
    }
}