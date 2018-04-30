using System;

namespace PhotoGallery.BL
{
    public interface IBaseDialogViewModel
    {
        string Title { get; set; }
        Action CloseAction { get; set; }
    }
}