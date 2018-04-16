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
            using (var DataContext = new DataContext())
            {
                DataContext.Photos.Any();
            }
        }

        private readonly AlbumRepository _albumRepository = new AlbumRepository(new DataContext());
        private readonly PhotoRepository _photoRepository = new PhotoRepository(new DataContext());
        private readonly ItemTagRepository _itemTagRepository = new ItemTagRepository(new DataContext());

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
            var album = _albumRepository.FindByTitle("Testing album");
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
        [Fact]
        public void FindByName_TestItemTag_NotNull()
        {
            var tag = _itemTagRepository.GetByName("Testing tag");
            Assert.NotNull(tag);
        }


    }
}


//        [Fact]
//        public void FindByName_ChocolateCake_ContainsEgg()
//        {
//            var recipe = recipeRepositorySUT.FindByName("Čokoládová torta");
//            var containsEgg = recipe.Ingredients.Any(ingredient => ingredient.Ingredient.Name == "Vajíčko");
//            Assert.True(containsEgg);
//        }

//        [Fact]
//        public void FindByName_Ingredient_NotNull()
//        {
//            var recipe = recipeRepositorySUT.FindByName("Čokoládová torta");
//            var containsEgg = recipe.Ingredients.Any(ingredient => ingredient.Ingredient.Name == "Vajíčko");
//            Assert.True(containsEgg);
//        }
//    }
//}
