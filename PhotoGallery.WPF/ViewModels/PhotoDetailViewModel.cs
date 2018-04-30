using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using PhotoGallery.BL;
using PhotoGallery.BL.IoC;
using PhotoGallery.BL.MessengerFile.Messeges;
using PhotoGallery.BL.Models;
using PhotoGallery.WPF.ViewModels.Base;
using PhotoGallery.WPF.ViewModels.Dialogs;

namespace PhotoGallery.WPF.ViewModels
{
    public class PhotoDetailViewModel : BaseViewModel
    {
        private readonly IMessenger _messenger;
        private readonly IUnitOfWork _unitOfWork;
        private Point _mousePoint;
        private PhotoDetailModel _photo;
        public string PhotoPath { get; set; }
        public string PhotoName { get; set; }
        public ObservableCollection<TagModel> Tags { get; set; }

        private List<int> _photosIds;
        public int CurrentPhotoIndex { get; set; }
        public int NumOfPhotos { get; set; }
        public string Pages => $"{CurrentPhotoIndex+1} / {NumOfPhotos}";

        public ICommand PreviousPhotoCommand { get; set; }
        public ICommand NextPhotoCommand { get; set; }
        public ICommand AddTagCommand { get; set; }

        // TODO oznacovanie tagov na fotke
        public PhotoDetailViewModel(IMessenger messenger, IUnitOfWork unitOfWork)
        {
            _messenger = messenger;
            _unitOfWork = unitOfWork;

            PreviousPhotoCommand = new RelayCommand(GetPreviousPhoto, GetPreviousPhotosCanUse);
            NextPhotoCommand = new RelayCommand(GetNextPhoto, GetNextPhotosCanUse);
            AddTagCommand = new RelayCommand(ShowPopup);

            _messenger.Register<SendFilterWithPhoto>(OnLoad);
            _messenger.Register<SendNewPhotoName>(msg => PhotoName = msg.PhotoName);
            _messenger.Register<SendNewTag>(msg => AddTag(msg.TagModel));
        }

        public void AddTag(TagModel tag)
        {
            tag.XPosition = (int)_mousePoint.X;
            tag.YPosition = (int)_mousePoint.Y;
            
            tag.Id = tag.IsItem ? _unitOfWork.ItemTags.Add(tag, _photo) : _unitOfWork.PersonTags.Add(tag, _photo);

            Tags.Add(tag);
        }

        private void ShowPopup(object obj)
        {
            if (obj == null) return;
            _mousePoint = Mouse.GetPosition(obj as UIElement);
            IoC.Ui.ShowAddTag(new AddTagDialogViewModel(_messenger){ Title = "Add Tag" });
        }

        private void GetPreviousPhoto()
        {
            _photo = _unitOfWork.Photos.GetDetailModelById(_photosIds[--CurrentPhotoIndex]);
            SendPhotoToDetailVm(_photo);
            SetPhotoProperties(_photo);
        }

        public bool GetPreviousPhotosCanUse()
        {
            return CurrentPhotoIndex > 0 && NumOfPhotos > 1;
        }

        private void GetNextPhoto()
        {
            _photo = _unitOfWork.Photos.GetDetailModelById(_photosIds[++CurrentPhotoIndex]);
            SendPhotoToDetailVm(_photo);
            SetPhotoProperties(_photo);
        }

        public bool GetNextPhotosCanUse()
        {
            return CurrentPhotoIndex < NumOfPhotos-1 && NumOfPhotos > 1;
        }

        private void OnLoad(SendFilterWithPhoto filterAndPhoto)
        {
            _photo = _unitOfWork.Photos.GetDetailModelById(filterAndPhoto.PhotoId);
            SendPhotoToDetailVm(_photo);
            SetPhotoProperties(_photo);
            // fetch filtered and sorted data which are from correct album or contains good tag
            _photosIds = _unitOfWork.Photos.GetSortedFilteredPhotosIds(filterAndPhoto.FilterSortSettings,
                filterAndPhoto.ChosenItem);
            // Zistenie aktualneho indexu
            CurrentPhotoIndex = _photosIds.IndexOf(_photo.Id);
            // Zistenie poctu fotiek
            NumOfPhotos = _photosIds.Count;
        }

        private void SetPhotoProperties(PhotoDetailModel photoDetailModel)
        {
            PhotoPath = photoDetailModel.Path;
            PhotoName = photoDetailModel.Name;
            Tags = new ObservableCollection<TagModel>(photoDetailModel.Tags);
        }

        private void SendPhotoToDetailVm(PhotoDetailModel photoDetail)
        {
            _messenger.Send(new SendDetailPhotoModel(photoDetail));
        }
    }
}