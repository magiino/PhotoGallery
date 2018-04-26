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
        private AlbumModel _selectedAlbum;

        public string AlbumName { get; set; }

        // Zoradenie
        public IEnumerable<Sort> Sorts { get; set; }

        private Sort _selectedSort = Sort.None;
        public Sort SelectedSort
        {
            get => _selectedSort;
            set
            {
                Filter();
                _selectedSort = value;
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
                Filter();
                _selectedFormat = value;
            }
        }

        public IEnumerable<ResolutionModel> Resolutions { get; set; }

        private ResolutionModel _selectedResolution;
        public ResolutionModel SelectedResolution
        {
            get => _selectedResolution;
            set
            {
                Filter();
                _selectedResolution = value;
            }
        }

        private DateTime _dateFrom = DateTime.MinValue;
        public DateTime DateFrom
        {
            get => _dateFrom;
            set
            {
                Filter();
                _dateFrom = value;
            }
        }

        private DateTime _dateTo = DateTime.MaxValue;
        public DateTime DateTo
        {
            get => _dateTo;
            set
            {
                Filter();
                _dateTo = value;
            }
        }

        public ICommand SetCoverPhotoCommand { get; }
        public ICommand ChangeAlbumNameCommand { get; }
        public ICommand AddPhotoCommand { get; }
        public ICommand DeletePhotoCommand { get; }

        public RightMenuViewModel(IMessenger messenger, IUnitOfWork unitOfWork)
        {
            _messenger = messenger;
            _unitOfWork = unitOfWork;
            OnLoad();

            SetCoverPhotoCommand = new RelayCommand(ChangeAlbumCoverPhoto, CanUseButton);
            ChangeAlbumNameCommand = new RelayCommand(ChangeAlbumName, ChangeAlbumNameCanUse);
            AddPhotoCommand = new RelayCommand(AddPhoto, AddPhotoCanUse);
            DeletePhotoCommand = new RelayCommand(DeletePhoto, CanUseButton);
            
            _messenger.Register<SendChosenPhoto>(msg => _selectedPhoto = _unitOfWork.Photos.GetDetailModelById(msg.PhotoId));
            _messenger.Register<ChosenItem>(SetAlbum);
        }


        private void AddPhoto()
        {
            var photoDetailModel = IoC.AddPhoto.ChoosePhoto();
            var photoListModel =_unitOfWork.Photos.Add(photoDetailModel);

            _messenger.Send(new SendAddPhoto(photoListModel, _selectedAlbum.Id));
        }

        private void DeletePhoto()
        {
            _unitOfWork.Photos.Delete(_selectedPhoto.Id);
            _messenger.Send(new SendDeletePhoto(_selectedPhoto.Id, _selectedAlbum.Id));
            _selectedPhoto = null;
        }

        private void ChangeAlbumCoverPhoto()
        {
            _selectedAlbum.CoverPhotoPath = _selectedPhoto.Path;
            _selectedAlbum.CoverPhotoId = _selectedPhoto.Id;
            _unitOfWork.Albums.Update(_selectedAlbum);
            _messenger.Send(new SendAlbum(_selectedAlbum));
        }

        private void ChangeAlbumName()
        {
            _selectedAlbum.Title = AlbumName;
            _unitOfWork.Albums.Update(_selectedAlbum);
            _messenger.Send(new SendAlbum(_selectedAlbum));
        }


        public bool CanUseButton()
        {
            return _selectedPhoto != null;
        }

        public bool AddPhotoCanUse()
        {
            return _selectedAlbum != null;
        }

        public bool ChangeAlbumNameCanUse()
        {
            return !string.IsNullOrEmpty(AlbumName);
        }


        private void SetAlbum(ChosenItem msg)
        {
            _selectedAlbum = msg.IsTag == false ? _unitOfWork.Albums.GetById(msg.Id) : null;
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
                ResolutionId = SelectedResolution.Id,
                DateFrom = DateFrom,
                DateTo = DateTo
            });
        }
    }
}