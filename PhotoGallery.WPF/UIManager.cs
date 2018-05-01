using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using PhotoGallery.BL;
using PhotoGallery.BL.Interfaces;
using PhotoGallery.BL.IoC;
using PhotoGallery.WPF.Views.Dialogs;

namespace PhotoGallery.WPF
{
    public class UiManager : IUiManager
    {
        public Task ShowAddTag(IBaseDialogViewModel viewModel)
        {
            return new AddTagDialog().ShowDialog(viewModel);
        }
    }
}