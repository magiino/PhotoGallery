using System.Data.Entity;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.DAL
{
    public class DataContext : DbContext
    {
        public DbSet<AlbumEntity> Albums { get; set; }
        public DbSet<PhotoEntity> Photos { get; set; }
        public DbSet<ItemTagEntity> ItemTags { get; set; }
        public DbSet<PersonTagEntity> PersonTags { get; set; }
        public DbSet<PersonEntity> Persons { get; set; }
        public DbSet<ResolutionEntity> Resolutions { get; set; }

        public DataContext() : base("name=PhotoGalleryContext") { }
    }
}
