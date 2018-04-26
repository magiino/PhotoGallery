using PhotoGallery.BL;
using PhotoGallery.BL.IoC;
using PhotoGallery.WPF.ViewModels;

namespace PhotoGallery.WPF
{
    public class ViewModelLocator
    {
        private readonly Messenger _messenger = IoC.Messenger;
        private readonly IUnitOfWork _unitOfWork = IoC.UnitOfWork;

        public IApplicationViewModel ApplicationViewModel { get; set; } = IoC.Application;

        public LeftMenuViewModel RecipeListViewModel => new LeftMenuViewModel(_messenger, _unitOfWork);
        public PhotoListViewModel PhotoListViewModel => new PhotoListViewModel(_messenger, _unitOfWork);
        public RightMenuViewModel RightMenuViewModel => new RightMenuViewModel(_messenger, _unitOfWork);
        public PhotoDetailViewModel PhotoDetailViewModel => new PhotoDetailViewModel(_messenger, _unitOfWork);
        public InfoViewModel InfoViewModel => new InfoViewModel(_messenger, _unitOfWork);
        
    }
}