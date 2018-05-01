using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.Interfaces
{
    public interface IAddPhoto
    {
        PhotoDetailModel ChoosePhoto();
    }
}