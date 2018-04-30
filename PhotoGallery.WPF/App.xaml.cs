using System.Windows;
using PhotoGallery.BL;
using PhotoGallery.BL.IoC;
using PhotoGallery.WPF.ViewModels;

namespace PhotoGallery.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SetUpApplication();

            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }

        private void SetUpApplication()
        {
            IoC.Kernel.Bind<IAddPhoto>().ToConstant(new AddPhoto());
            IoC.Kernel.Bind<IApplicationViewModel>().ToConstant(new ApplicationViewModel());
            IoC.Kernel.Bind<IUiManager>().ToConstant(new UiManager());

            IoC.SetUp();
        }
    }
}