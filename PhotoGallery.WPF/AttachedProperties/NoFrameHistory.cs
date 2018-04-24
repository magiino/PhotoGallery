using System.Windows;
using System.Windows.Controls;
using PhotoGallery.WPF.AttachedProperties.Base;

namespace PhotoGallery.WPF.AttachedProperties
{
    /// <summary>
    /// Keeps navigation history empty
    /// </summary>
    public class NoFrameHistory : BaseAttachedProperty<NoFrameHistory, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var frame = (sender as Frame);
            // Hide navigation bar
            frame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;

            // After navigation to other page clear history
            frame.Navigated += (ss, ee) => ((Frame)ss).NavigationService.RemoveBackEntry();
        }
    }
}