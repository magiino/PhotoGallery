using System.Windows.Controls;

namespace PhotoGallery.WPF.ViewModels.Dialogs
{
    public class WindowDialogViewModel
    {
        public string Title { get; set; }
        public double WindowMinimumWidth { get; set; } = 200;
        public double WindowMinimumHeight { get; set; } = 250;
        public Control Content { get; set; }
    }
}