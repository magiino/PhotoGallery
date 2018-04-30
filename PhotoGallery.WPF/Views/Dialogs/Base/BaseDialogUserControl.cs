using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PhotoGallery.BL;
using PhotoGallery.WPF.ViewModels.Dialogs;

namespace PhotoGallery.WPF.Views.Dialogs.Base
{
    public class BaseDialogUserControl : UserControl
    {

        private readonly DialogWindow _dialogWindow;

        public int WindowMinimumWidth { get; set; } = 250;
        public int WindowMinimumHeight { get; set; } = 100;
        public string Title { get; set; }

        public BaseDialogUserControl()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
                _dialogWindow = new DialogWindow {ViewModel = new WindowDialogViewModel()};
        }

        public Task ShowDialog<T>(T viewModel)
            where T : IBaseDialogViewModel
        {
            var tcs = new TaskCompletionSource<bool>();

            Application.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    _dialogWindow.ViewModel.WindowMinimumWidth = WindowMinimumWidth;
                    _dialogWindow.ViewModel.WindowMinimumHeight = WindowMinimumHeight;
                    _dialogWindow.ViewModel.Title = string.IsNullOrEmpty(viewModel.Title) ? Title : viewModel.Title;

                    _dialogWindow.ViewModel.Content = this;

                    viewModel.CloseAction = _dialogWindow.Close;
                        

                    DataContext = viewModel;

                    _dialogWindow.ShowDialog();
                }
                finally
                {
                    tcs.TrySetResult(true);
                }
            });

            return tcs.Task;
        }
    }
}