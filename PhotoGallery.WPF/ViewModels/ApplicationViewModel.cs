using PhotoGallery.BL;
using PhotoGallery.BL.Interfaces;
using PhotoGallery.DAL.Enums;
using PhotoGallery.WPF.ViewModels.Base;

namespace PhotoGallery.WPF.ViewModels
{
    /// <summary>
    /// The application state as a view model
    /// </summary>
    public class ApplicationViewModel : BaseViewModel , IApplicationViewModel
    {
        /// <summary>
        /// The current page of the application
        /// </summary>
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.PhotoList;

        /// <summary>
        /// Navigates to the specified page
        /// </summary>
        /// <param name="page">The page to go to</param>
        public void GoToPage(ApplicationPage page)
        {
            CurrentPage = page;
        }
    }
}
