using System;

namespace PhotoGallery.BL.Interfaces
{
    public interface IBaseDialogViewModel
    {
        string Title { get; set; }
        Action CloseAction { get; set; }
    }
}