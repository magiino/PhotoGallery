using System.Windows;
using PhotoGallery.WPF.ViewModels.Dialogs;

namespace PhotoGallery.WPF.Views.Dialogs
{
    public partial class DialogWindow : Window
    {
        private WindowDialogViewModel _viewModel;

        public WindowDialogViewModel ViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
                DataContext = _viewModel;
            }
        }

        public DialogWindow()
        {
            InitializeComponent();
        }
    }
}