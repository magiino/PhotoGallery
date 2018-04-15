using PhotoGallery.BL.Models;
using System.Collections.ObjectModel;
using PhotoGallery.BL;
using PhotoGallery.BL.MessengerFile.Messeges;

namespace PhotoGallery.WPF.ViewModels
{
    public class LeftMenuViewModel
    {
        private readonly IMessenger _messenger;
        private readonly IUnitOfWork _unitOfWork;
        private ItemTagListModel _selectedItemTag;
        private PersonTagListModel _selectedPersonTag;
        private AlbumModel _selectedAlbum;
        public ObservableCollection<AlbumModel> Albums { get; set; }

        public AlbumModel SelectedAlbum
        {
            get => _selectedAlbum;
            set
            {
                SelectedAlbum = null;
                SelectedPersonTag = null;
                _messenger.Send(new ChoosenItem(value.Id, false));
                _selectedAlbum = value;
            }
        }


        public ObservableCollection<ItemTagListModel> Items { get; set; }

        public ItemTagListModel SelectedItemTag
        {
            get => _selectedItemTag;
            set
            {
                SelectedAlbum = null;
                SelectedPersonTag = null;
                _messenger.Send(new ChoosenItem(value.Id, true));
                _selectedItemTag = value;
            }
        }

        public ObservableCollection<PersonTagListModel> Persons { get; set; }
        public PersonTagListModel SelectedPersonTag
        {
            get => _selectedPersonTag;
            set
            {
                SelectedAlbum = null;
                _selectedItemTag = null;
                _messenger.Send(new ChoosenItem(value.Id, true));
                _selectedPersonTag = value;
            }
        }

        public LeftMenuViewModel(IMessenger messenger, IUnitOfWork unitOfWork)
        {
            _messenger = messenger;
            _unitOfWork = unitOfWork;

            OnLoad();
        }

        private void OnLoad()
        {
            // fetch data
            Albums = new ObservableCollection<AlbumModel>(_unitOfWork.Albums.GetAll());
            Items = new ObservableCollection<ItemTagListModel>(_unitOfWork.ItemTags.GetAll());
            Persons = new ObservableCollection<PersonTagListModel>(_unitOfWork.PersonTags.GetAll());
        }
    }
}