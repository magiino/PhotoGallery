using System;
using PhotoGallery.WPF.ViewModels.Base;
using PhotoGallery.BL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Input;
using PhotoGallery.BL;
using PhotoGallery.BL.IoC;
using PhotoGallery.BL.MessengerFile.Messeges;
using PhotoGallery.DAL.Enums;

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
        public ICommand AddPhotoCommand { get; }
        public ICommand DeletePhotoCommand { get; }

        public RightMenuViewModel(IMessenger messenger, IUnitOfWork unitOfWork)
        {
            _messenger = messenger;
            _unitOfWork = unitOfWork;
            OnLoad();

            SetCoverPhotoCommand = new RelayCommand(ChangeAlbumCoverPhoto, CanUseButton);
            AddPhotoCommand = new RelayCommand(AddPhoto, AddPhotoCanUse);
            DeletePhotoCommand = new RelayCommand(DeletePhoto, CanUseButton);
            
            _messenger.Register<SendChosenPhoto>(msg => _selectedPhoto = unitOfWork.Photos.GetDetailModelById(msg.PhotoId));
            _messenger.Register<SendChosenItem>(SetAlbum);
        }


        private void AddPhoto()
        {
            var photo = IoC.AddPhoto.ChoosePhoto(_selectedAlbum.Id);
            _unitOfWork.Photos.Add(photo);
            // TODO zaradit fotku do listu
        }
        private void DeletePhoto()
        {
            _unitOfWork.Photos.Delete(_selectedPhoto.Id);
            _messenger.Send(new SendDeletePhoto(_selectedPhoto.Id));
        }
        private void ChangeAlbumCoverPhoto()
        {
            _selectedAlbum.CoverPhotoPath = _selectedPhoto.Path;
            _unitOfWork.Albums.Update(_selectedAlbum);
        }


        public bool CanUseButton()
        {
            return _selectedPhoto != null;
        }

        public bool AddPhotoCanUse()
        {
            return _selectedAlbum != null;
        }


        private void SetAlbum(SendChosenItem msg)
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
            _messenger.Send(new SendFilterSettings()
            {
                SearchString = FilterByName,
                Sort = SelectedSort,
                SortAscending = SortAscending,
                Format = SelectedFormat,
                ResolutionModel = SelectedResolution,
                DateFrom = DateFrom,
                DateTo = DateTo
            });
        }
    }
}