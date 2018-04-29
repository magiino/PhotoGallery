using System;
using PhotoGallery.BL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using PhotoGallery.BL;
using PhotoGallery.BL.IoC;
using PhotoGallery.BL.MessengerFile.Messeges;
using PhotoGallery.DAL.Enums;
using PhotoGallery.WPF.ViewModels.Base;

namespace PhotoGallery.WPF.ViewModels
{
    public class RightMenuViewModel : BaseViewModel
    {
        private readonly IMessenger _messenger;
        private readonly IUnitOfWork _unitOfWork;

        private PhotoDetailModel _selectedPhoto;

        public AlbumModel SelectedAlbum { get; set; }
        public string AlbumName { get; set; }

        // Zoradenie
        public IEnumerable<Sort> Sorts { get; set; }

        private Sort _selectedSort = Sort.None;
        public Sort SelectedSort
        {
            get => _selectedSort;
            set
            {
                _selectedSort = value;
                Filter();
            }
        }

        public bool SortAscending { get; set; } = true;

        // Filtrovanie
        public string FilterByName { get; set; }

        public IEnumerable<Format> Formats { get; set; }

        private Format _selectedFormat = Format.None;
        public Format SelectedFormat
        {
            get => _selectedFormat;
            set
            {
                _selectedFormat = value;
                Filter();
            }
        }

        public IEnumerable<ResolutionModel> Resolutions { get; set; }

        private ResolutionModel _selectedResolution;
        public ResolutionModel SelectedResolution
        {
            get => _selectedResolution;
            set
            {
                _selectedResolution = value;
                Filter();
            }
        }

        private DateTime _dateFrom = DateTime.Now - TimeSpan.FromDays(365*10);
        public DateTime DateFrom
        {
            get => _dateFrom;
            set
            {
                _dateFrom = value;
                Filter();
            }
        }

        private DateTime _dateTo = DateTime.Today;
        public DateTime DateTo
        {
            get => _dateTo;
            set
            {
                _dateTo = value;
                Filter();
            }
        }

        public ICommand SetCoverPhotoCommand { get; }
        public ICommand ChangeAlbumNameCommand { get; }
        public ICommand AddPhotoCommand { get; }
        public ICommand DeletePhotoCommand { get; }
        public ICommand RunFilterCommand { get; }

        public RightMenuViewModel(IMessenger messenger, IUnitOfWork unitOfWork)
        {
            _messenger = messenger;
            _unitOfWork = unitOfWork;
            OnLoad();

            SetCoverPhotoCommand = new RelayCommand(ChangeAlbumCoverPhoto, CanUseButton);
            ChangeAlbumNameCommand = new RelayCommand(ChangeAlbumName, ChangeAlbumNameCanUse);
            AddPhotoCommand = new RelayCommand(AddPhoto, AddPhotoCanUse);
            DeletePhotoCommand = new RelayCommand(DeletePhoto, CanUseButton);
            RunFilterCommand = new RelayCommand(() =>
            {
                if (string.IsNullOrEmpty(FilterByName)) return;
                Filter();
            });


            _messenger.Register<SendChosenPhoto>(msg => _selectedPhoto = _unitOfWork.Photos.GetDetailModelById(msg.PhotoId));
            _messenger.Register<ChosenItem>(SetAlbum);
        }


        private void AddPhoto()
        {
            var photoDetailModel = IoC.AddPhoto.ChoosePhoto();
            photoDetailModel.AlbumId = SelectedAlbum.Id;
            var photoListModel =_unitOfWork.Photos.Add(photoDetailModel);

            _messenger.Send(new SendAddPhoto(photoListModel, SelectedAlbum.Id));
        }

        private void DeletePhoto()
        {
            _unitOfWork.Photos.Delete(_selectedPhoto.Id);
            _messenger.Send(new SendDeletePhoto(_selectedPhoto.Id, SelectedAlbum.Id));
            _selectedPhoto = null;
        }

        private void ChangeAlbumCoverPhoto()
        {
            SelectedAlbum.CoverPhotoPath = _selectedPhoto.Path;
            SelectedAlbum.CoverPhotoId = _selectedPhoto.Id;
            _unitOfWork.Albums.Update(SelectedAlbum);
            _messenger.Send(new SendAlbum(SelectedAlbum));
        }

        private void ChangeAlbumName()
        {
            SelectedAlbum.Title = AlbumName;
            _unitOfWork.Albums.Update(SelectedAlbum);
            _messenger.Send(new SendAlbum(SelectedAlbum));
        }


        public bool CanUseButton()
        {
            return _selectedPhoto != null;
        }

        public bool AddPhotoCanUse()
        {
            return SelectedAlbum != null;
        }

        public bool ChangeAlbumNameCanUse()
        {
            return !string.IsNullOrEmpty(AlbumName);
        }


        private void SetAlbum(ChosenItem msg)
        {
            SelectedAlbum = msg.IsTag == false ? _unitOfWork.Albums.GetById(msg.Id) : null;
            AlbumName = SelectedAlbum?.Title;
        }

        private void OnLoad()
        {
            Sorts = Enum.GetValues(typeof(Sort)).Cast<Sort>();
            Formats = Enum.GetValues(typeof(Format)).Cast<Format>();
            Resolutions = _unitOfWork.Resolutions.GetAll();
        }

        private void Filter()
        {
            _messenger.Send(new FilterSortSettings()
            {
                SearchString = FilterByName,
                Sort = SelectedSort,
                SortAscending = SortAscending,
                Format = SelectedFormat,
                ResolutionId = SelectedResolution?.Id ?? -1,
                DateFrom = DateFrom,
                DateTo = DateTo
            });
        }
    }
}