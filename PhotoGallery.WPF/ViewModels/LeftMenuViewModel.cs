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
        private ItemTagListModel _selectedItemTag;
        private PersonTagListModel _selectedPersonTag;
        private AlbumModel _selectedAlbum;
        public ObservableCollection<AlbumModel> Albums { get; set; }

        public AlbumModel SelectedAlbum
        {
            get => _selectedAlbum;
            set
            {
                SelectedItemTag = null;
                SelectedPersonTag = null;
                _messenger.Send(new SendChoosenItem(value.Id, false));
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
                _messenger.Send(new SendChoosenItem(value.Id, true));
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
                _messenger.Send(new SendChoosenItem(value.Id, true));
                _selectedPersonTag = value;
            }
        }

        public ICommand DeleteAlbumCommand { get; set; }
        public ICommand AddAlbumCommand { get; set; }
        public ICommand DeleteTagCommand { get; set; }

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
            return _selectedPersonTag != null || _selectedItemTag != null;
        }

        private void DeleteTag()
        {
            if(_selectedPersonTag != null)
                _unitOfWork.PersonTags.Delete(_selectedPersonTag.Id);
            if (_selectedItemTag != null)
                _unitOfWork.PersonTags.Delete(_selectedItemTag.Id);
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
            Items = new ObservableCollection<ItemTagListModel>(_unitOfWork.ItemTags.GetAll());
            Persons = new ObservableCollection<PersonTagListModel>(_unitOfWork.PersonTags.GetAll());
        }
    }
}