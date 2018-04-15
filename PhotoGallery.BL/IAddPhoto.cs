using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL
{
    public interface IAddPhoto
    {
        PhotoEntity ChoosePhoto(int albumId);
    }
}