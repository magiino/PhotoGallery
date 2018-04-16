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
    public class FilterViewModel : BaseViewModel
    {
        private readonly IMessenger _messenger;
        private readonly IUnitOfWork _unitOfWork;

        private PhotoDetailModel _selectedPhoto;
        private AlbumModel _selectedAlbum;

        public string AlbumName { get; set; }

        // Zoradenie
        public bool SortByName { get; set; } = false;
        public bool SortByDate { get; set; } = false;
        public bool SortByFormat { get; set; } = true;

        public bool SortAscending { get; set; } = true;
        public bool SortDescending { get; set; } = true;

        // Filtrovanie
        public string FilterByName { get; set; }

        public IEnumerable<Format> FormatList { get; set; }
        public Format SelectedFormat { get; set; } = Format.None;

        public IEnumerable<ResolutionModel> Resolutions { get; set; }
        public ResolutionModel SelectedResolution { get; set; }

        public DateTime DateFrom { get; set; } = DateTime.MinValue;
        public DateTime DateTo { get; set; } = DateTime.MaxValue;

        public ICommand SetCoverPhotoCommand { get; }
        public ICommand AddPhotoCommand { get; }
        public ICommand DeletePhotoCommand { get; }

        public FilterViewModel(IMessenger messenger, IUnitOfWork unitOfWork)
        {
            _messenger = messenger;
            _unitOfWork = unitOfWork;
            OnLoad();

            SetCoverPhotoCommand = new RelayCommand(ChangeAlbumCoverPhoto, CanUseButton);
            AddPhotoCommand = new RelayCommand(AddPhoto, CanUseButton);
            DeletePhotoCommand = new RelayCommand(DeletePhoto, CanUseButton);
            
            _messenger.Register<SendChoosenPhoto>(msg => _selectedPhoto = unitOfWork.Photos.GetById(msg.PhotoId));
            _messenger.Register<SendChoosenItem>(SetAlbum);
        }

        private void AddPhoto()
        {
            var photo = IoC.AddPhoto.ChoosePhoto(_selectedAlbum.Id);
            _unitOfWork.Photos.Add(photo);
            // TODO zaradit fotku do listu
        }
        private void DeletePhoto()
        {
            // TODO zmazat ju aj lokalne
            _unitOfWork.Photos.Delete(_selectedPhoto.Id);
        }
        private void ChangeAlbumCoverPhoto()
        {
            _selectedAlbum.CoverPhotoPath = _selectedPhoto.Path;
            _unitOfWork.Albums.Update(_selectedAlbum);
        }

        private bool CanUseButton()
        {
            return _selectedPhoto != null;
        }

        private void SetAlbum(SendChoosenItem msg)
        {
            // TODO set album
            // msg.IsTag == false ?  : (_selectedAlbum = null)
        }
        private void OnLoad()
        {
            FormatList = Enum.GetValues(typeof(Format)).Cast<Format>();
            // Resolutions =  
        }
        private void PhotoFilter()
        {
            // TODO filter
            Expression<Func<PhotoDetailModel, bool>> predicate = x => (
                (x.CreatedTime >= DateFrom && x.CreatedTime <= DateTo) 
                && x.Resolution == SelectedResolution
                && x.Format == SelectedFormat);
        }
    }
}