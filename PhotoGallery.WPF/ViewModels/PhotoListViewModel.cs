using System;
using System.Collections.Generic;
using PhotoGallery.BL.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using PhotoGallery.BL;
using PhotoGallery.BL.IoC;
using PhotoGallery.BL.MessengerFile.Messeges;
using PhotoGallery.DAL.Enums;
using PhotoGallery.WPF.ViewModels.Base;

namespace PhotoGallery.WPF.ViewModels
{
    public class PhotoListViewModel : BaseViewModel
    {
        private readonly IMessenger _messenger;
        private readonly IUnitOfWork _unitOfWork;
        private PhotoListModel _selectedPhoto;

        private FilterSortSettings _filterSortSettings;
        private ChosenItem _chosenItem;

        public int PageIndex { get; set; }
        public int AllPages { get; set; }
        private int _numOfPhotos;
        private List<int> _photoIds;

        public string Pages => $"{PageIndex} / {AllPages}";

        public ObservableCollection<PhotoListModel> Photos { get; set; } = new ObservableCollection<PhotoListModel>();

        public PhotoListModel SelectedPhoto
        {
            get => _selectedPhoto;
            set
            {
                _selectedPhoto = value;
                if (_selectedPhoto == null) return;
                _messenger.Send(new SendChosenPhoto(SelectedPhoto.Id));
            }
        }

        public ICommand PreviousPageCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand ShowDetailPhotoCommand { get; }

        public PhotoListViewModel(IMessenger messenger, IUnitOfWork unitOfWork)
        {
            _messenger = messenger;
            _unitOfWork = unitOfWork;

            _filterSortSettings = new FilterSortSettings()
            {
                Format = Format.None,
                DateFrom = DateTime.Now - TimeSpan.FromDays(365 * 10),
                DateTo = DateTime.Today,
                SearchString = string.Empty,
                Sort = Sort.None,
                SortAscending = true,
                ResolutionId = -1
            };

            NextPageCommand = new RelayCommand(GetNextPhotos, GetNextPhotosCanUse);
            PreviousPageCommand = new RelayCommand(GetPreviousPhotos, GetPreviousPhotosCanUse);
            ShowDetailPhotoCommand = new RelayCommand(ShowDetailPhoto);

            _messenger.Register<ChosenItem>(msg =>
            {
                _chosenItem = msg;
                FilterPhotos(_filterSortSettings);
            });
            _messenger.Register<FilterSortSettings>(FilterPhotos);
            _messenger.Register<SendDeletePhoto>(msg =>
            {
                var photo = Photos.SingleOrDefault(x => x.Id == msg.PhotoId);
                if (photo == null) return;
                Photos.Remove(photo);
                --_numOfPhotos;
                CalcPages();
            });
            _messenger.Register<SendAddPhoto>(msg =>
            {
                Photos.Add(msg.PhotoModel);
                ++_numOfPhotos;
                CalcPages();
            });
        }

        private void GetNextPhotos()
        {
            ++PageIndex;
            FetchPhotos();
        }

        private void GetPreviousPhotos()
        {
            --PageIndex;
            FetchPhotos();
        }

        private void ShowDetailPhoto()
        {
            IoC.Application.GoToPage(ApplicationPage.PhotoDetail);
            _messenger.Send(new SendFilterWithPhoto(_selectedPhoto.Id, _filterSortSettings, _chosenItem));
        }

        public bool GetNextPhotosCanUse()
        {
            return PageIndex <= AllPages && AllPages > 1;
        }

        public bool GetPreviousPhotosCanUse()
        {
            return PageIndex <= 1 && AllPages > 1;
        }

        private void FilterPhotos(FilterSortSettings filterSortSettings)
        {
            _filterSortSettings = filterSortSettings;
            PageIndex = 1;
            _photoIds = _unitOfWork.Photos.GetSortedFilteredPhotosIds(_filterSortSettings, _chosenItem);
            _numOfPhotos = _photoIds.Count;
            FetchPhotos();
            CalcPages();
        }

        private void FetchPhotos()
        {
            Photos.Clear();
            foreach (var photoId in _photoIds.Skip((PageIndex - 1) * IoC.PageSize).Take(IoC.PageSize))
                Photos.Add(_unitOfWork.Photos.GetListModelById(photoId));
        }

        private void CalcPages()
        {
            var num = (double)_numOfPhotos / IoC.PageSize;
            AllPages = (int)Math.Ceiling(num);
        }
    }
}