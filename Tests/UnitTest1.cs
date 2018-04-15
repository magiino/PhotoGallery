using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoGallery.BL.Repositories;
using Xunit;
using PhotoGallery.DAL;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DbConnectionTest()
        {
            using (var DataContext = new DataContext())
            {
                DataContext.Photos.Any();
            }
        }

        private AlbumRepository albumRepository = new AlbumRepository();
        private PhotoRepository photoRepository = new PhotoRepository();
        private ItemTagRepository itemTagRepository = new ItemTagRepository();

        [Fact]
        public void FindByName_TestPhoto_NotNull()
        {
            var photo = photoRepository.FindByName("Testing photo");
            Assert.IsNotNull(photo);
        }
        [Fact]
        public void FindByName_NonExistPhoto_Null()
        {
            var photo = photoRepository.FindByName("NonExisting Photo");
            Assert.IsNull(photo);
        }
        [Fact]
        public void FindByName_TestAlbum_NotNull()
        {
            var album = albumRepository.FindByTitle("Testing album");
            Assert.IsNotNull(album);
        }
        [Fact]
        public void FindByName_TestPhoto_ContainsNote()
        {
            var photo = photoRepository.FindByName("Testing photo");
            var note = photo.Note.Contains("testing note");
            Assert.IsTrue(note);
        }
        [Fact]
        public void FindByName_TestPhoto_Resolution()
        {
            var photo = photoRepository.FindByName("Testing photo");
            var resol = photo.Resolution.Height == 600;
            Assert.IsTrue(resol);
        }
        [Fact]
        public void FindByName_TestItemTag_NotNull()
        {
            var tag = itemTagRepository.GetByName("Testing tag");
            Assert.IsNotNull(tag);
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
