using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using PhotoGallery.BL;
using PhotoGallery.DAL.Entities;
using PhotoGallery.DAL.Enums;

namespace PhotoGallery.WPF
{
    public class AddPhoto : IAddPhoto
    {
        private static Regex r = new Regex(":");

        // TODO prerobit na model
        public PhotoEntity ChoosePhoto(int albumId)
        {
            var fileDialog = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.bmp, *.png, *.Tif, *.Tiff) | *.jpg; *.jpeg; *.jpe; *.bmp; *.png; *.Tif; *.Tiff",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (fileDialog.ShowDialog() != true) return null;
            
            var path = fileDialog.FileName;
            var photo = new PhotoEntity
            {
                Name = Path.GetFileName(path),
                Path = path,
                Note = fileDialog.Title,
                AlbumId = albumId,
                Format = GetFileFormat(path),
                CreatedTime = GetCreatedTime(path),
                Resolution = GetResolution(path),
            };

            return photo;
        }

        private ResolutionEntity GetResolution(string path)
        {
            var img = Image.FromFile(path);

            return new ResolutionEntity()
            {
                Height = img.Height,
                Width = img.Width,
            };
        }

        private DateTime GetCreatedTime(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (Image myImage = Image.FromStream(fs, false, false))
            {
                PropertyItem propItem = null;
                try
                {
                    propItem = myImage.GetPropertyItem(36867);
                }
                catch { }
                if (propItem != null)
                {
                    string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                    return DateTime.Parse(dateTaken);
                }
                else
                    return new FileInfo(path).LastWriteTime;
            }
        }

        private Format GetFileFormat(string path)
        {
            var extension = Path.GetExtension(path);
            switch (extension)
            {
                case ".bmp":
                    return Format.Bmp;
                case ".jpg":
                    return Format.Jpg;
                case ".jpe":
                    return Format.Jpe;
                case ".jpeg":
                    return Format.Jpeg;
                case ".png":
                    return Format.Png;
                case ".tif":
                    return Format.Tif;
                case ".tiff":
                    return Format.Tiff;
                default:
                    Debugger.Break();
                    throw new NotImplementedException();
            }
        }
    }
}