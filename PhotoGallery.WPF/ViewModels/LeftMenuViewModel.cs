using PhotoGallery.BL.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using PhotoGallery.BL;
using PhotoGallery.BL.Interfaces;
using PhotoGallery.BL.IoC;
using PhotoGallery.BL.MessengerFile.Messeges;
using PhotoGallery.DAL.Enums;
using PhotoGallery.WPF.ViewModels.Base;

namespace PhotoGallery.WPF.ViewModels
{
    public class LeftMenuViewModel : BaseViewModel
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
                GoToPage();

                SelectedItem = null;
                SelectedPerson = null;
                _selectedAlbum = value;
                if (value == null) return;
                _messenger.Send(new ChosenItem(value.Id, ItemType.Album));
            }
        }

        public string NewAlbumName { get; set; }
        private bool _albumsAreExpanded;
        public bool AlbumsAreExpanded
        {
            get => _albumsAreExpanded;
            set
            {
                PersonsAreExpanded = false;
                ItemsAreExpanded = false;
                _albumsAreExpanded = value;
            }
        }

        public ICommand DeleteAlbumCommand { get; }
        public ICommand AddAlbumCommand { get; }
        #endregion

        #region Item
        public ObservableCollection<ItemModel> Items { get; set; }
        public ObservableCollection<ItemModel> FilteredItems { get; set; }
        private ItemModel _selectedItem;
        public ItemModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                GoToPage();

                SelectedAlbum = null;
                SelectedPerson = null;
                _selectedItem = value;

                if (value == null) return;
                _messenger.Send(new ChosenItem(value.Id, ItemType.Item));
            }
        }
        public string ItemSearch { get; set; }
        private bool _itemsAreExpanded;
        public bool ItemsAreExpanded
        {
            get => _itemsAreExpanded;
            set
            {
                AlbumsAreExpanded = false;
                PersonsAreExpanded = false;
                _itemsAreExpanded = value;
            }
        }

        public ICommand SearchItemCommand { get; }
        public ICommand DeleteItemCommand { get; }
        #endregion

        #region Person
        public ObservableCollection<PersonModel> Persons { get; set; }
        public ObservableCollection<PersonModel> FilteredPersons { get; set; }
        private PersonModel _selectedPerson;
        public PersonModel SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                GoToPage();

                SelectedAlbum = null;
                SelectedItem = null;
                _selectedPerson = value;
                if (value == null) return;
                _messenger.Send(new ChosenItem(value.Id, ItemType.Person));
            }
        }
        public string PersonSearch { get; set; }
        private bool _personsAreExpanded;
        public bool PersonsAreExpanded
        {
            get => _personsAreExpanded;
            set
            {
                AlbumsAreExpanded = false;
                ItemsAreExpanded = false;
                _personsAreExpanded = value;
            }
        }
        public ICommand SearchPersonCommand { get; }
        public ICommand DeletePersonCommand { get; }
        #endregion

        public ICommand ShowListPageCommand { get; }

        public LeftMenuViewModel(IMessenger messenger, IUnitOfWork unitOfWork)
        {
            _messenger = messenger;
            _unitOfWork = unitOfWork;

            OnLoad();

            DeleteAlbumCommand = new RelayCommand(DeleteAlbum, DeleteAlbumCanUse);
            AddAlbumCommand = new RelayCommand(AddAlbum, AddAlbumCanUse);
            DeleteItemCommand = new RelayCommand(DeleteItem, DeleteItemCanUse);
            DeletePersonCommand = new RelayCommand(DeletePerson, DeletePersonCanUse);
            SearchItemCommand = new RelayCommand(SearchForItem);
            SearchPersonCommand = new RelayCommand(SearchForPerson);
            ShowListPageCommand = new RelayCommand(GoToPage);

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
            _messenger.Register<SendNewTag>(AddNewPersonItemToList);
        }


        private void SearchForPerson()
        {
            if (string.IsNullOrEmpty(PersonSearch))
            {
                FilteredPersons = Persons;
                return;
            }
            FilteredPersons = new ObservableCollection<PersonModel>(Persons.Where(p => p.FirstName.Contains(PersonSearch) || p.LastName.Contains(PersonSearch)));
            PersonSearch = "";
        }

        private void SearchForItem()
        {
            if (string.IsNullOrEmpty(ItemSearch))
            {
                FilteredItems = Items;
                return;
            }
            FilteredItems = new ObservableCollection<ItemModel>(Items.Where(i => i.Name.Contains(ItemSearch)));
            ItemSearch = "";
        }

        private void DeleteItem()
        {
            _unitOfWork.Items.Delete(_selectedItem.Id);
            Items.Remove(_selectedItem);
        }

        private void DeletePerson()
        {
            _unitOfWork.Persons.Delete(_selectedPerson.Id);

            Persons.Remove(_selectedPerson);
        }

        private void AddAlbum()
        {
            var newAlbum = new AlbumModel()
            {
                NumberOfPhotos = 0,
                Title = NewAlbumName,
            };

            Albums.Add(_unitOfWork.Albums.Add(newAlbum));
            NewAlbumName = string.Empty;
        }

        private void AddNewPersonItemToList(SendNewTag newTag)
        {
            FilteredItems = Items = new ObservableCollection<ItemModel>(_unitOfWork.Items.GetAll());
            FilteredPersons = Persons = new ObservableCollection<PersonModel>(_unitOfWork.Persons.GetAll());
        }

        private void DeleteAlbum()
        {
            var albumPhotos = _unitOfWork.Photos.GetPhotosByPredicate(x => x.AlbumId == _selectedAlbum.Id);
            foreach (var photos in albumPhotos)
                _unitOfWork.Photos.Delete(photos.Id);

            _unitOfWork.Albums.Delete(_selectedAlbum.Id);
            _messenger.Send(new SendAlbum(_selectedAlbum, true));
            Albums.Remove(_selectedAlbum);
        }

        private bool DeleteItemCanUse()
        {
            return _selectedItem != null && _unitOfWork.ItemTags.GetByItemId(_selectedItem.Id).Count == 0;
        }

        private bool DeletePersonCanUse()
        {
            return _selectedPerson != null && _unitOfWork.PersonTags.GetByPersonId(_selectedPerson.Id).Count == 0; ;
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
            if (msg.Delete) return;
            var album = Albums.SingleOrDefault(x => x.Id == msg.AlbumModel.Id);
            if (album == null) return;
            album.Title = msg.AlbumModel.Title;
            album.CoverPhotoPath = msg.AlbumModel.CoverPhotoPath;
            album.CoverPhotoId = msg.AlbumModel.CoverPhotoId;
        }

        private void OnLoad()
        {
            Albums = new ObservableCollection<AlbumModel>(_unitOfWork.Albums.GetAll());
            FilteredItems =  Items = new ObservableCollection<ItemModel>(_unitOfWork.Items.GetAll());
            FilteredPersons = Persons = new ObservableCollection<PersonModel>(_unitOfWork.Persons.GetAll());
        }

        private void GoToPage()
        {
            if (IoC.Application.CurrentPage != ApplicationPage.PhotoList)
                IoC.Application.GoToPage(ApplicationPage.PhotoList);
        }
    }
}