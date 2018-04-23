using System;
using PhotoGallery.BL.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Ninject.Infrastructure.Language;
using PhotoGallery.BL;
using PhotoGallery.BL.IoC;
using PhotoGallery.BL.MessengerFile.Messeges;
using PhotoGallery.DAL.Entities;

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
        private int Id { get; set; }

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

        public ICommand PreviousPageCommand { get; set; }
        public ICommand NextPageCommand { get; set; }

        public PhotoListViewModel(IMessenger messenger, IUnitOfWork unitOfWork)
        {
            _messenger = messenger;
            _unitOfWork = unitOfWork;

            NextPageCommand = new RelayCommand(GetNextPhotos);
            PreviousPageCommand = new RelayCommand(GetPreviousPhotos);

            _messenger.Register<SendChosenItem>(SelectedItemChanged);

        }

        private void GetPreviousPhotos()
        {
            
        }

        private void GetNextPhotos()
        {
            throw new System.NotImplementedException();
        }

        private void SelectedItemChanged(SendChosenItem item)
        {
            PageIndex = 1;
            if (item.IsTag)
            {
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
                Photos = new ObservableCollection<PhotoListModel>(_unitOfWork.Photos.GetPhotosByPageFilter(x => x.AlbumId == item.Id, PageIndex));

            var num = (double)Photos.Count / IoC.PageSize;
            AllPages = (int)Math.Ceiling(num);
        }
    }
}