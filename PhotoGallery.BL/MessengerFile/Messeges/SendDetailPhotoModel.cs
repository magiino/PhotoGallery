using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.MessengerFile.Messeges
{
    public class SendDetailPhotoModel
    {
        public PhotoDetailModel PhotoDetailModel { get; set; }

        public SendDetailPhotoModel(PhotoDetailModel photoDetailModel)
        {
            PhotoDetailModel = photoDetailModel;
        }
    }
}