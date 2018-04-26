using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using PhotoGallery.BL;
using PhotoGallery.BL.IoC;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Enums;

namespace PhotoGallery.WPF
{
    public class AddPhoto : IAddPhoto
    {
        private static Regex r = new Regex(":");

        public PhotoDetailModel ChoosePhoto()
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
            var photo = new PhotoDetailModel()
            {
                Name = Path.GetFileName(path),
                Path = path,
                Note = fileDialog.Title,
                Format = GetFileFormat(path),
                CreatedTime = GetCreatedTime(path),
                Resolution = GetResolution(path),
            };

            return photo;
        }

        private ResolutionModel GetResolution(string path)
        {
            var img = Image.FromFile(path);
            var resolution = IoC.UnitOfWork.Resolutions.GetByWidthAndHeight(img.Height, img.Width);
            if (resolution != null) return resolution;

            var resolutionModel = new ResolutionModel()
            {
                Height = img.Height,
                Width = img.Width,
            };
            resolutionModel.Id = IoC.UnitOfWork.Resolutions.Add(resolutionModel).Id;
            return resolutionModel;
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
                    return Format.None;
            }
        }
    }
}