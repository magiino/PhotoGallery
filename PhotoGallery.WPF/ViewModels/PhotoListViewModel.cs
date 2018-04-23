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

        public PhotoListViewModel(IMessenger messenger, IUnitOfWork unitOfWork)
        {
            _messenger = messenger;
            _unitOfWork = unitOfWork;

            NextPageCommand = new RelayCommand(GetNextPhotos, GetNextPhotosCanUse);
            PreviousPageCommand = new RelayCommand(GetPreviousPhotos, GetPreviousPhotosCanUse);

            _messenger.Register<SendChosenItem>(SelectedItemChanged);
            _messenger.Register<SendFilterSettings>(FilterPhotos);
            _messenger.Register<SendDeletePhoto>(msg =>
            {
                var photo = Photos.SingleOrDefault(x => x.Id == msg.PhotoId);
                Photos.Remove(photo);
            });
            _messenger.Register<SendAddPhoto>(msg => Photos.Add(msg.PhotoModel));
        }

        private void GetNextPhotos()
        {

        }

        private void GetPreviousPhotos()
        {
            
        }


        public bool GetNextPhotosCanUse()
        {
            return PageIndex <= AllPages;
        }

        public bool GetPreviousPhotosCanUse()
        {
            return PageIndex != 0;
        }

        private void FilterPhotos(SendFilterSettings filterSettings)
        {
            // TODO filtracia
        }


        private void SelectedItemChanged(SendChosenItem item)
        {
            PageIndex = 1;
            if (item.IsTag)
            {
                _selectedAlbum = null;

                var personTag = _unitOfWork.PersonTags.GetById(item.Id);
                if (personTag == null)
                {
                    var itemTag = _unitOfWork.ItemTags.GetById(item.Id);
                    Photos = new ObservableCollection<PhotoListModel>(_unitOfWork.Photos.GetPhotosByItemTag(itemTag, PageIndex));
                }
                else
                    Photos = new ObservableCollection<PhotoListModel>(_unitOfWork.Photos.GetPhotosByPersonTag(personTag, PageIndex));
            }
            else
            {
                Photos = new ObservableCollection<PhotoListModel>(
                    _unitOfWork.Photos.GetPhotosByPageFilter(x => x.AlbumId == item.Id, PageIndex));

                _selectedAlbum = _unitOfWork.Albums.GetById(item.Id);
            }

            var num = (double)Photos.Count / IoC.PageSize;
            AllPages = (int)Math.Ceiling(num);
        }
    }
}