using System.Threading.Tasks;
using PhotoGallery.BL.Interfaces;

namespace PhotoGallery.BL.IoC
{
    public interface IUiManager
    {
        Task ShowAddTag(IBaseDialogViewModel viewModel);
    }
}