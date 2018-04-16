using System;
using PhotoGallery.WPF.ViewModels.Base;
using PhotoGallery.BL.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Input;
using PhotoGallery.BL;
using PhotoGallery.BL.MessengerFile.Messeges;
using PhotoGallery.DAL.Enums;

namespace PhotoGallery.WPF.ViewModels
{
    public class FilterViewModel : BaseViewModel
    {
        private readonly IMessenger _messenger;
        private readonly IUnitOfWork _unitOfWork;

        // TODO property na upravovanie nazvu albumu
        public string AlbumName { get; set; }
        // TODO zmenit cover photo
        public int CoverPhotoId { get; set; }

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

        private PhotoDetailModel _selectedPhoto;
        private AlbumModel _selectedAlbum;

        private ICommand SetCoverPhotoCommand { get; set; }

        public FilterViewModel(IMessenger messenger, IUnitOfWork unitOfWork)
        {
            _messenger = messenger;
            _unitOfWork = unitOfWork;
            OnLoad();

            SetCoverPhotoCommand = new RelayCommand(ChangeAlbumCoverPhoto);

            _messenger.Register<SendChoosenPhoto>(msg => _selectedPhoto = unitOfWork.Photos.GetById(msg.PhotoId));
            _messenger.Register<SendChoosenItem>(SetAlbum);
        }

        private void SetAlbum(SendChoosenItem msg)
        {
            // TODO set album
           //msg.IsTag == false ?  : (_selectedAlbum = null)
        }

        private void OnLoad()
        {
            FormatList = Enum.GetValues(typeof(Format)).Cast<Format>();
            // Resolutions =  
        }

        private void ChangeAlbumCoverPhoto()
        {
            _selectedAlbum.CoverPhotoPath = _selectedPhoto.Path;
            _unitOfWork.Albums.Update(_selectedAlbum);
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