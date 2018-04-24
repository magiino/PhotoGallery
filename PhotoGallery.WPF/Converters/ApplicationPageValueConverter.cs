using System;
using System.Diagnostics;
using System.Globalization;
using PhotoGallery.DAL.Enums;
using PhotoGallery.WPF.Views;

namespace PhotoGallery.WPF.Converters
{
    /// <summary>
    /// Converts the <see cref="ApplicationPage"/> to an actual view/page
    /// </summary>
    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Find the page
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.PhotoList:
                    return new PhotoListPage();
                case ApplicationPage.PhotoDetail:
                    return new PhotoDetailPage();
                default:
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
