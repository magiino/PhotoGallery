using System.Threading.Tasks;

namespace PhotoGallery.BL.IoC
{
    public interface IUiManager
    {
        Task ShowAddTag(IBaseDialogViewModel viewModel);
    }
}