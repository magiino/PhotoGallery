using PhotoGallery.BL.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using PhotoGallery.BL;
using PhotoGallery.BL.MessengerFile.Messeges;

namespace PhotoGallery.WPF.ViewModels
{
    public class LeftMenuViewModel
    {
        private readonly IMessenger _messenger;
        private readonly IUnitOfWork _unitOfWork;

        #region Album
        public ObservableCollection<AlbumModel> Albums { get; set; }

        private AlbumModel _selectedAlbum;
        public AlbumModel SelectedAlbum
        {
            get => _selectedAlbum;
            set
            {
                SelectedItem = null;
                SelectedPerson = null;
                _messenger.Send(new ChosenItem(value.Id, false));
                _selectedAlbum = value;
            }
        }

        public string NewAlbumName { get; set; }
        #endregion

        #region Item
        public ObservableCollection<ItemModel> Items { get; set; }
        private ItemModel _selectedItem;
        public ItemModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                SelectedAlbum = null;
                SelectedPerson = null;
                _messenger.Send(new ChosenItem(value.Id, true));
                _selectedItem = value;
            }
        }
        public string ItemSearch { get; set; }
        #endregion

        #region Person
        public ObservableCollection<PersonModel> Persons { get; set; }
        private PersonModel _selectedPerson;
        public PersonModel SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                SelectedAlbum = null;
                _selectedItem = null;
                _messenger.Send(new ChosenItem(value.Id, true));
                _selectedPerson = value;
            }
        }
        public string PersonSearch { get; set; } 
        #endregion

        public ICommand DeleteAlbumCommand { get; }
        public ICommand AddAlbumCommand { get; }

        // TODO zmazat tak ze sa zmazu aj vsetky tagy a to iste aj ked zmazem album zmazu sa vsetkz fotky
        public ICommand DeletePersonCommand { get; }
        public ICommand DeleteItemCommand { get; }

        public LeftMenuViewModel(IMessenger messenger, IUnitOfWork unitOfWork)
        {
            _messenger = messenger;
            _unitOfWork = unitOfWork;

            OnLoad();

            DeleteAlbumCommand = new RelayCommand(DeleteAlbum, DeleteAlbumCanUse);
            AddAlbumCommand = new RelayCommand(AddAlbum, AddAlbumCanUse);
            DeleteItemCommand = new RelayCommand(DeleteItem, DeleteItemCanUse);
            DeletePersonCommand = new RelayCommand(DeletePerson, DeletePersonCanUse);

            _messenger.Register<SendDeletePhoto>(msg =>
            {
                if (SelectedAlbum == null) return;
                var album = Albums.FirstOrDefault(x => x.Id == msg.AlbumId);
                album.NumberOfPhotos--;
            });
            _messenger.Register<SendAddPhoto>(msg =>
            {
                if (SelectedAlbum == null) return;
                var album = Albums.FirstOrDefault(x => x.Id == msg.AlbumId);
                album.NumberOfPhotos++;
            });

            _messenger.Register<SendAlbum>(ChangeAlbum);
        }

        private void DeleteItem()
        {
            _unitOfWork.PersonTags.Delete(_selectedItem.Id);
        }

        private void DeletePerson()
        {
            _unitOfWork.PersonTags.Delete(_selectedPerson.Id);
        }

        private void AddAlbum()
        {
            var newAlbum = new AlbumModel()
            {
                NumberOfPhotos = 0,
                Title = NewAlbumName,
            };

            Albums.Add(_unitOfWork.Albums.Add(newAlbum));
        }

        private void DeleteAlbum()
        {
            _unitOfWork.Albums.Delete(_selectedAlbum.Id);
            Albums.Remove(_selectedAlbum);
        }


        private bool DeleteItemCanUse()
        {
            return _selectedItem != null;
        }

        private bool DeletePersonCanUse()
        {
            return _selectedPerson != null;
        }

        public bool AddAlbumCanUse()
        {
            return !string.IsNullOrEmpty(NewAlbumName);
        }

        private bool DeleteAlbumCanUse()
        {
            return SelectedAlbum != null;
        }

        private void ChangeAlbum(SendAlbum msg)
        {
            var album = Albums.FirstOrDefault(x => x.Id == msg.AlbumModel.Id);

            album.Title = msg.AlbumModel.Title;
            album.CoverPhotoPath = msg.AlbumModel.CoverPhotoPath;
            album.CoverPhotoId = msg.AlbumModel.CoverPhotoId;
        }

        private void OnLoad()
        {
            Albums = new ObservableCollection<AlbumModel>(_unitOfWork.Albums.GetAll());
            Items = new ObservableCollection<ItemModel>(_unitOfWork.Items.GetAll());
            Persons = new ObservableCollection<PersonModel>(_unitOfWork.Persons.GetAll());
        }
    }
}