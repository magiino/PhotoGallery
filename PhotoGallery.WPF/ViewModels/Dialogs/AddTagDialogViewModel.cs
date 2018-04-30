using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PhotoGallery.BL;
using PhotoGallery.BL.MessengerFile.Messeges;
using PhotoGallery.BL.Models;
using PhotoGallery.WPF.ViewModels.Dialogs.Base;

namespace PhotoGallery.WPF.ViewModels.Dialogs
{
    public class AddTagDialogViewModel : BaseDialogViewModel
    {
        private readonly IMessenger _messenger;
        private readonly IUnitOfWork _unitOfWork;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public bool IsThing { get; set; }

        // TODO dat na vyber z existujucich osob
        public ObservableCollection<ItemModel> Items;
        public ObservableCollection<PersonModel> Persons;

        public ICommand AddItemCommand { get; }
        public ICommand CancelCommand { get; }

        public AddTagDialogViewModel(IMessenger messenger, IUnitOfWork unitOfWork)
        {
            _messenger = messenger;
            _unitOfWork = unitOfWork;

            Items = new ObservableCollection<ItemModel>(unitOfWork.Items.GetAll());
            Persons = new ObservableCollection<PersonModel>(unitOfWork.Persons.GetAll());

            AddItemCommand = new RelayCommand(AddItem);
            CancelCommand = new RelayCommand(CloseAction);
        }

        private void AddItem(object parameter)
        {
            _messenger.Send(new SendNewTag(new TagModel()
            {
                IsItem = IsThing,
                Name = IsThing ? Name : $"{FirstName} {LastName}",
            }));
            CloseAction();
        }
    }
}