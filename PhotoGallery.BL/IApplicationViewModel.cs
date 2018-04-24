using PhotoGallery.DAL.Enums;

namespace PhotoGallery.BL
{
    public interface IApplicationViewModel
    {
        ApplicationPage CurrentPage { get; }
        void GoToPage(ApplicationPage page);
    }
}