using PhotoGallery.BL.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PhotoGallery.BL;
using PhotoGallery.BL.MessengerFile.Messeges;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.WPF.ViewModels
{
    public class LeftMenuViewModel
    {
        private readonly IMessenger _messenger;
        private readonly IUnitOfWork _unitOfWork;

        private ItemListModel _selectedItemListTag;
        private PersonListModel _selectedPerson;
        private AlbumModel _selectedAlbum;

        public ObservableCollection<AlbumModel> Albums { get; set; }
        public AlbumModel SelectedAlbum
        {
            get => _selectedAlbum;
            set
            {
                SelectedItemListTag = null;
                SelectedPerson = null;
                _messenger.Send(new SendChoosenItem(value.Id, false));
                _selectedAlbum = value;
            }
        }

        public ObservableCollection<ItemListModel> Items { get; set; }
        public ItemListModel SelectedItemListTag
        {
            get => _selectedItemListTag;
            set
            {
                SelectedAlbum = null;
                SelectedPerson = null;
                _messenger.Send(new SendChoosenItem(value.Id, true));
                _selectedItemListTag = value;
            }
        }

        public ObservableCollection<PersonListModel> Persons { get; set; }
        public PersonListModel SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                SelectedAlbum = null;
                _selectedItemListTag = null;
                _messenger.Send(new SendChoosenItem(value.Id, true));
                _selectedPerson = value;
            }
        }

        public ICommand DeleteAlbumCommand { get; }
        public ICommand AddAlbumCommand { get; }
        public ICommand DeleteTagCommand { get; }

        public LeftMenuViewModel(IMessenger messenger, IUnitOfWork unitOfWork)
        {
            _messenger = messenger;
            _unitOfWork = unitOfWork;

            OnLoad();

            DeleteAlbumCommand = new RelayCommand(DeleteAlbum, DeleteAlbumCanUse);
            AddAlbumCommand = new RelayCommand(AddAlbum);
            DeleteTagCommand = new RelayCommand(DeleteTag, DeleteTagCanUse);
        }

        private bool DeleteTagCanUse()
        {
            return _selectedPerson != null || _selectedItemListTag != null;
        }

        private void DeleteTag()
        {
            if(_selectedPerson != null)
                _unitOfWork.PersonTags.Delete(_selectedPerson.Id);
            if (_selectedItemListTag != null)
                _unitOfWork.PersonTags.Delete(_selectedItemListTag.Id);
        }

        private void AddAlbum()
        {
            // TODO upravovanie albumu
            // TODO pridavanie albumu z ineho okna?
            _unitOfWork.Albums.Add(new AlbumEntity()
            {

            });
        }

        private bool DeleteAlbumCanUse()
        {
            return SelectedAlbum != null;
        }

        private void DeleteAlbum()
        {
            _unitOfWork.Albums.Delete(_selectedAlbum.Id);
            Albums.Remove(_selectedAlbum);
        }

        private void OnLoad()
        {
            // fetch data
            Albums = new ObservableCollection<AlbumModel>(_unitOfWork.Albums.GetAll());

            //Items = new ObservableCollection<ItemListModel>(_unitOfWork.ItemTags.GetAll());
            Persons = new ObservableCollection<PersonListModel>(_unitOfWork.PersonTags.GetAll());
        }
    }
}