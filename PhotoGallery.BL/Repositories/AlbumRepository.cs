using System.Collections.Generic;
using System.Linq;

namespace PhotoGallery.BL.Repositories
{

    
    using PhotoGallery.DAL;
    using PhotoGallery.BL.Models;
    using PhotoGallery.BL;
    public class AlbumRepository 
    {


        private readonly Mapper mapper = new Mapper();

        public AlbumModel FindByTitle(string title)

        {
            using (var dataContext = new DataContext())
            {
                var album = dataContext
                .Albums
                .FirstOrDefault(r => r.Title == title);
                return this.mapper.Map(album);

            }

        }

        public ICollection<AlbumModel> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return this.mapper.Map(dataContext.Albums.ToList());

            }

        }

        public AlbumModel GetById(int id)

        {
            using (var dataContext = new DataContext())
            {
                var album = dataContext
                 .Albums
                 .FirstOrDefault(r => r.Id == id);
                return this.mapper.Map(album);
            }

        }
    }
}
