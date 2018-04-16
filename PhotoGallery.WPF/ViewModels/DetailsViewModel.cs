using System;
using System.Windows.Input;
using PhotoGallery.BL;
using PhotoGallery.BL.MessengerFile.Messeges;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Enums;

namespace PhotoGallery.WPF.ViewModels
{
    public class DetailsViewModel
    {
        private readonly IMessenger _messenger;
        private readonly IUnitOfWork _unitOfWork;

        public PhotoDetailModel SelectedPhoto { get; set; }
        public string Name { get; set; }
        public DateTime CreatedTime { get; set; }
        public Format Format { get; set; }
        public ResolutionModel Resolution { get; set; }
        public string Note { get; set; }
        public string Location { get; set; }

        public ICommand AddTagCommand { get; }
        public ICommand DeleteTagCommand { get; }

        public DetailsViewModel(IMessenger messenger, IUnitOfWork unitOfWork)
        {
            _messenger = messenger;
            _unitOfWork = unitOfWork;

            OnLoad();

            _messenger.Register<SendChoosenPhoto>(msg => SelectedPhoto = unitOfWork.Photos.GetDetailModelById(msg.PhotoId));
        }

        private void OnLoad()
        {
            // fetch data
        }
    }
}