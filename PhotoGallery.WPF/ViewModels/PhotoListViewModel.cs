using PhotoGallery.BL.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PhotoGallery.BL;
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
        private int Id { get; set; }

        public ObservableCollection<PhotoListModel> Photos { get; set; }

        public PhotoListModel SelectedPhoto
        {
            get => _selectedPhoto;
            set
            {
                _messenger.Send(new SendChoosenPhoto(SelectedPhoto.Id));
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

            _messenger.Register<SendChoosenItem>(SelectedItemChanged);

        }

        private void GetPreviousPhotos()
        {
            
        }

        private void GetNextPhotos()
        {
            throw new System.NotImplementedException();
        }

        private void SelectedItemChanged(SendChoosenItem item)
        {
            PageIndex = 1;
            if (item.IsTag)
            {
                //Photos = new ObservableCollection<PhotoListModel>(_unitOfWork.Photos.GetPhotosPredicate(x =>
                //{
                //    foreach (var tag in x.Tags)
                //        return tag.Id == item.Id;
                //},1));
            }
            else
                Photos = new ObservableCollection<PhotoListModel>(_unitOfWork.Photos.GetPhotosByPageFilter(x => x.AlbumId == item.Id, PageIndex));
        }
    }
}