using PhotoGallery.DAL.Enums;

namespace PhotoGallery.BL.Interfaces
{
    public interface IApplicationViewModel
    {
        ApplicationPage CurrentPage { get; }
        void GoToPage(ApplicationPage page);
    }
}