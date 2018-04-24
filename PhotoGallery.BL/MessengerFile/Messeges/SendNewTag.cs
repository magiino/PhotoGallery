using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.MessengerFile.Messeges
{
    public class SendNewTag
    {
        public TagModel TagModel { get; set; }

        public SendNewTag(TagModel tagModel)
        {
            TagModel = tagModel;
        }
    }
}