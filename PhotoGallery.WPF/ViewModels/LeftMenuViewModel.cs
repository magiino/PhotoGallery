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
                _messenger.Send(new ChosenItem(value.Id, false));
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
                _messenger.Send(new ChosenItem(value.Id, true));
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
        public ICommand SearchPersonCommand { get; }
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

                _selectedAlbum = null;
                _selectedItem = null;
                _selectedPerson = value;

                if (value == null) return;
                _messenger.Send(new ChosenItem(value.Id, true));
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

        // TODO zmazat tak ze sa zmazu aj vsetky tagy a to iste aj ked zmazem album zmazu sa vsetkz fotky
        public ICommand DeletePersonCommand { get; }
        public ICommand DeleteItemCommand { get; }
        #endregion

        public LeftMenuViewModel(IMessenger messenger, IUnitOfWork unitOfWork)
        {
            _messenger = messenger;
            _unitOfWork = unitOfWork;

            OnLoad();

            DeleteAlbumCommand = new RelayCommand(DeleteAlbum, DeleteAlbumCanUse);
            AddAlbumCommand = new RelayCommand(AddAlbum, AddAlbumCanUse);
            DeleteItemCommand = new RelayCommand(DeleteItem, DeleteItemCanUse);
            DeletePersonCommand = new RelayCommand(DeletePerson, DeletePersonCanUse);
            SearchItemCommand = new RelayCommand(SearchForItem, SearchForItemCanUse);
            SearchPersonCommand = new RelayCommand(SearchForPerson, SearchForPersonCanUse);

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
            _messenger.Register<SendNewTag>(AddNewTagToList);
        }


        private void SearchForPerson()
        {
            FilteredPersons = new ObservableCollection<PersonModel>(Persons.Where(p => p.FirstName.Contains(PersonSearch) || p.LastName.Contains(PersonSearch)));
            PersonSearch = "";
        }

        private void SearchForItem()
        {
            FilteredItems = new ObservableCollection<ItemModel>(Items.Where(i => i.Name.Contains(ItemSearch)));
            ItemSearch = "";
        }

        // TODO ak sa zmaze osoba zmazu sa aj vsetky tagy
        private void DeleteItem()
        {
            _unitOfWork.Items.Delete(_selectedItem.Id);
        }

        private void DeletePerson()
        {
            _unitOfWork.Persons.Delete(_selectedPerson.Id);
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

        private void AddNewTagToList(SendNewTag newTag)
        { 
            var tag = newTag.TagModel;
            if (tag.IsItem)
            {
                var item = Items.FirstOrDefault(x => x.Id == tag.ItemId);
                if(item == null)
                    Items.Add(_unitOfWork.Items.GetById(tag.ItemId));
            }
            else
            {
                var person = Persons.FirstOrDefault(x => x.Id == tag.PersonId);
                if (person == null)
                    Persons.Add(_unitOfWork.Persons.GetById(tag.PersonId));
            }
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

        private bool SearchForItemCanUse()
        {
            return !string.IsNullOrEmpty(ItemSearch);
        }
        private bool SearchForPersonCanUse()
        {
            return !string.IsNullOrEmpty(PersonSearch); ;
        }

        private void ChangeAlbum(SendAlbum msg)
        {
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