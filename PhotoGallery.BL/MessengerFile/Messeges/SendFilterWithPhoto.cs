namespace PhotoGallery.BL.MessengerFile.Messeges
{
    public class SendFilterWithPhoto
    {
        public int PhotoId { get; set; }
        public FilterSortSettings FilterSortSettings { get; set; }
        public ChosenItem ChosenItem { get; set; }

        public SendFilterWithPhoto(int photoId, FilterSortSettings filterSortSettings, ChosenItem chosenItem)
        {
            PhotoId = photoId;
            FilterSortSettings = filterSortSettings;
            ChosenItem = chosenItem;
        }
    }
}