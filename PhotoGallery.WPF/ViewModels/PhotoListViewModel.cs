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

        public int PageIndex { get; set; } = 1;
        public int AllPages { get; set; }
        public int NumOfPhotos { get; set; }
        public List<int> PhotoIds { get; set; }

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

            _filterSortSettings = new FilterSortSettings()
            {
                Format = Format.None,
                DateFrom = DateTime.MinValue,
                DateTo = DateTime.MaxValue,
                SearchString = String.Empty,
                Sort = Sort.None,
                SortAscending = true,
                ResolutionId = 0
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
                --NumOfPhotos;
                CalcPages();
            });
            _messenger.Register<SendAddPhoto>(msg =>
            {
                Photos.Add(msg.PhotoModel);
                ++NumOfPhotos;
                CalcPages();
            });
        }

        private void GetNextPhotos()
        {
            ++PageIndex;
            FetchPhotos();
            //if (_chosenItem.IsTag)
            //    FetchTagPhotos(_chosenItem.Id);
            //else
            //    FetchAlbumPhotos(_chosenItem.Id);

        }

        private void GetPreviousPhotos()
        {
            --PageIndex;
            FetchPhotos();
            //if (_chosenItem.IsTag)
            //    FetchTagPhotos(_chosenItem.Id);
            //else
            //    FetchAlbumPhotos(_chosenItem.Id);
        }

        private void ShowDetailPhoto()
        {
            IoC.Application.GoToPage(ApplicationPage.PhotoDetail);
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
            PageIndex = 1;
            PhotoIds = _unitOfWork.Photos.GetSortedFilteredPhotosIds(_filterSortSettings, _chosenItem);
            NumOfPhotos = PhotoIds.Count;
            FetchPhotos();
            CalcPages();
        }

        private void FetchPhotos()
        {
            foreach (var photoId in PhotoIds.Skip((PageIndex - 1) * IoC.PageSize).Take(IoC.PageSize))
                Photos.Add(_unitOfWork.Photos.GetListModelById(photoId));
        }

        private void CalcPages()
        {
            var num = (double)NumOfPhotos / IoC.PageSize;
            AllPages = (int)Math.Ceiling(num);
        }
    }
}

// TODO tieto komentare nemazat este!!
//private void SelectedItemChanged(ChosenItem item)
//{

//    if (item.IsTag)
//    {
//        SelectedAlbum = null;
//        FetchTagPhotos(item.Id);
//        NumOfPhotos = _unitOfWork.Photos.GetNumberOfPhotosWithThisTag(item.Id);
//    }
//    else
//    {
//        FetchAlbumPhotos(item.Id);
//        SelectedAlbum = _unitOfWork.Albums.GetById(item.Id);
//        NumOfPhotos = SelectedAlbum.NumberOfPhotos;
//    }
//}

//private void FetchTagPhotos(int id)
//{
//    var personTag = _unitOfWork.PersonTags.GetById(id);
//    if (personTag == null)
//    {
//        var itemTag = _unitOfWork.ItemTags.GetById(id);
//        Photos = new ObservableCollection<PhotoListModel>(_unitOfWork.Photos.GetPhotosByItemTag(itemTag, PageIndex));
//    }
//    else
//        Photos = new ObservableCollection<PhotoListModel>(_unitOfWork.Photos.GetPhotosByPersonTag(personTag, PageIndex));
//}

//private void FetchAlbumPhotos(int id)
//{
//    Photos = new ObservableCollection<PhotoListModel>(
//        _unitOfWork.Photos.GetPhotosByPageFilter(x => x.AlbumId == id, PageIndex));
//}