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

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public bool IsThing { get; set; }

        public ICommand AddItemCommand { get; }
        public ICommand CancelCommand { get; }

        public AddTagDialogViewModel(IMessenger messenger)
        {
            _messenger = messenger;

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