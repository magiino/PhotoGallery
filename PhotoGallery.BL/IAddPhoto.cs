using PhotoGallery.BL.Models;

namespace PhotoGallery.BL
{
    public interface IAddPhoto
    {
        PhotoDetailModel ChoosePhoto();
    }
}