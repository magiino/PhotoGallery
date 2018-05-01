using System.Linq;
using PhotoGallery.BL.Repositories;
using Xunit;
using PhotoGallery.DAL;

namespace Tests
{
    public class RepositoriesTests
    {
        public void DbConnectionTest()
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Photos.Any();
            }
        }

        private readonly AlbumRepository _albumRepository = new AlbumRepository(new DataContext());
        private readonly PhotoRepository _photoRepository = new PhotoRepository(new DataContext());

        [Fact]
        public void FindByName_TestPhoto_NotNull()
        {
            var photo = _photoRepository.GetByName("Testing photo");
            Assert.NotNull(photo);
        }
        [Fact]
        public void FindByName_NonExistPhoto_Null()
        {
            var photo = _photoRepository.GetByName("NonExisting Photo");
            Assert.Null(photo);
        }
        [Fact]
        public void FindByName_TestAlbum_NotNull()
        {
            var album = _albumRepository.GetByTitle("Testing album");
            Assert.NotNull(album);
        }
        [Fact]
        public void FindByName_TestPhoto_ContainsNote()
        {
            var photo = _photoRepository.GetByName("Testing photo");
            var note = photo.Note.Contains("testing note");
            Assert.True(note);
        }
        [Fact]
        public void FindByName_TestPhoto_Resolution()
        {
            var photo = _photoRepository.GetByName("Testing photo");
            var resol = photo.Resolution.Height == 600;
            Assert.True(resol);
        }
    }
}