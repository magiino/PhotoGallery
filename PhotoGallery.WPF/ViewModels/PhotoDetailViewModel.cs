using System.Collections.Generic;
using System.Windows.Input;
using PhotoGallery.BL;
using PhotoGallery.BL.MessengerFile.Messeges;
using PhotoGallery.BL.Models;

namespace PhotoGallery.WPF.ViewModels
{
    public class PhotoDetailViewModel
    {
        private readonly IMessenger _messenger;
        private readonly IUnitOfWork _unitOfWork;
        public PhotoDetailModel Photo { get; set; }
        public List<int> PhotosIds { get; set; }

        public int CurrentPhotoIndex { get; set; }
        public int NumOfPhotos { get; set; }

        public ICommand PreviousPhoto { get; set; }
        public ICommand NextPhoto { get; set; }

        // TODO oznacovanie tagov na fotke
        public PhotoDetailViewModel(IMessenger messenger, IUnitOfWork unitOfWork)
        {
            _messenger = messenger;
            _unitOfWork = unitOfWork;

            // TODO alebo tie nastavenia predavat cez konstruktor
            //OnLoad();
            PreviousPhoto = new RelayCommand(GetPreviousPhoto, GetPreviousPhotosCanUse);
            NextPhoto = new RelayCommand(GetNextPhoto, GetNextPhotosCanUse);

            _messenger.Register<SendFilterWithPhoto>(OnLoad);
        }

        private void GetPreviousPhoto()
        {
            Photo = _unitOfWork.Photos.GetDetailModelById(PhotosIds[++CurrentPhotoIndex]);
            SendPhotoToDetailVm(Photo);
        }

        private void GetNextPhoto()
        {
            Photo = _unitOfWork.Photos.GetDetailModelById(PhotosIds[--CurrentPhotoIndex]);
            SendPhotoToDetailVm(Photo);
        }

        public bool GetNextPhotosCanUse()
        {
            return CurrentPhotoIndex <= NumOfPhotos;
        }

        public bool GetPreviousPhotosCanUse()
        {
            return CurrentPhotoIndex >= 0;
        }

        private void OnLoad(SendFilterWithPhoto filterAndPhoto)
        {
            // TODO photo list mdoel prerobit asi na photo detail model
            Photo = _unitOfWork.Photos.GetDetailModelById(filterAndPhoto.PhotoId);
            SendPhotoToDetailVm(Photo);
            // fetch filtered and sorted data which are from correct album or contains good tag
            PhotosIds = _unitOfWork.Photos.GetSortedFilteredPhotosIds(filterAndPhoto.FilterSortSettings, filterAndPhoto.ChosenItem);
            // Zistenie aktualneho indexu
            CurrentPhotoIndex = PhotosIds.IndexOf(Photo.Id);
            // Zistenie poctu fotiek
            NumOfPhotos = PhotosIds.Count;
        }

        private void SendPhotoToDetailVm(PhotoDetailModel photoDetail)
        {
            _messenger.Send(new SendDetailPhotoModel(photoDetail));
        }
    }
}