namespace PhotoGallery.WPF.ViewModels.Base
{
    class BaseViewModel
    {

        [AddINotifyPropertyChangedInterface]
        public class BaseViewModel : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        }
    }
}
