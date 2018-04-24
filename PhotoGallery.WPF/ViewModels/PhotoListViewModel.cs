using System;
using PhotoGallery.BL.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using PhotoGallery.BL;
using PhotoGallery.BL.IoC;
using PhotoGallery.BL.MessengerFile.Messeges;

namespace PhotoGallery.WPF.ViewModels
{
    public class PhotoListViewModel
    {
        private readonly IMessenger _messenger;
        private readonly IUnitOfWork _unitOfWork;
        private PhotoListModel _selectedPhoto;
        private AlbumModel _selectedAlbum;

        private FilterSortSettings _filterSortSettings;
        private ChosenItem _chosenItem;

        public int PageIndex { get; set; } = 1;
        public int AllPages { get; set; }

        public ObservableCollection<PhotoListModel> Photos { get; set; }

        public PhotoListModel SelectedPhoto
        {
            get => _selectedPhoto;
            set
            {
                _messenger.Send(new SendChosenPhoto(SelectedPhoto.Id));
                _selectedPhoto = value;
            }
        }

        public ICommand PreviousPageCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand ShowDetailPhotoCommand { get; }

        public PhotoListViewModel(IMessenger messenger, IUnitOfWork unitOfWork)
        {
            _messenger = messenger;
            _unitOfWork = unitOfWork;

            NextPageCommand = new RelayCommand(GetNextPhotos, GetNextPhotosCanUse);
            PreviousPageCommand = new RelayCommand(GetPreviousPhotos, GetPreviousPhotosCanUse);
            ShowDetailPhotoCommand = new RelayCommand(ShowDetailPhoto);

            _messenger.Register<ChosenItem>(SelectedItemChanged);
            _messenger.Register<FilterSortSettings>(FilterPhotos);
            _messenger.Register<SendDeletePhoto>(msg =>
            {
                var photo = Photos.SingleOrDefault(x => x.Id == msg.PhotoId);
                Photos.Remove(photo);
                CalcPages();
            });
            _messenger.Register<SendAddPhoto>(msg => Photos.Add(msg.PhotoModel));
        }

        private void GetNextPhotos()
        {
            ++PageIndex;

            if (_chosenItem.IsTag)
                FetchTagPhotos(_chosenItem.Id);
            else
                FetchAlbumPhotos(_chosenItem.Id);

        }

        private void GetPreviousPhotos()
        {
            --PageIndex;
            if (_chosenItem.IsTag)
                FetchTagPhotos(_chosenItem.Id);
            else
                FetchAlbumPhotos(_chosenItem.Id);
        }

        private void ShowDetailPhoto()
        {
            // TODO chod na druhu stranku (tj detail)
            // Odosli filterSort a aktualnu fotku
            _messenger.Send(new SendFilterWithPhoto(_selectedPhoto.Id, _filterSortSettings, _chosenItem));
        }


        public bool GetNextPhotosCanUse()
        {
            return PageIndex <= AllPages;
        }

        public bool GetPreviousPhotosCanUse()
        {
            return PageIndex != 0;
        }

        private void FilterPhotos(FilterSortSettings filterSortSettings)
        {
            _filterSortSettings = filterSortSettings;
            // TODO filtracia
        }


        private void SelectedItemChanged(ChosenItem item)
        {
            PageIndex = 1;

            var numberOfPhotos = 0;

            if (item.IsTag)
            {
                _selectedAlbum = null;
                FetchTagPhotos(item.Id);
                // TODO get number of all phtoos with this tag
            }
            else
            {
                FetchAlbumPhotos(item.Id);

                 _selectedAlbum = _unitOfWork.Albums.GetById(item.Id);
            }

            CalcPages(numberOfPhotos);
        }

        private void FetchTagPhotos(int id)
        {
            var personTag = _unitOfWork.PersonTags.GetById(id);
            if (personTag == null)
            {
                var itemTag = _unitOfWork.ItemTags.GetById(id);
                Photos = new ObservableCollection<PhotoListModel>(_unitOfWork.Photos.GetPhotosByItemTag(itemTag, PageIndex));
            }
            else
                Photos = new ObservableCollection<PhotoListModel>(_unitOfWork.Photos.GetPhotosByPersonTag(personTag, PageIndex));
        }

        private void FetchAlbumPhotos(int id)
        {
            Photos = new ObservableCollection<PhotoListModel>(
                _unitOfWork.Photos.GetPhotosByPageFilter(x => x.AlbumId == id, PageIndex));
        }

        private void CalcPages(int numberOfPhotos)
        {
            var num = (double)numberOfPhotos / IoC.PageSize;
            AllPages = (int)Math.Ceiling(num);
        }
    }
}