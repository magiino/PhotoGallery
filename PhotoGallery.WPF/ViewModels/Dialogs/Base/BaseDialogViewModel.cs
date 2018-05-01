using System;
using PhotoGallery.BL;
using PhotoGallery.BL.Interfaces;
using PhotoGallery.WPF.ViewModels.Base;

namespace PhotoGallery.WPF.ViewModels.Dialogs.Base
{
    /// <summary>
    /// A base view model for any dialogs
    /// </summary>
    public class BaseDialogViewModel : BaseViewModel, IBaseDialogViewModel
    {
        #region Public Properties
        /// <summary>
        /// The title of the dialog
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Command for closing dialog window
        /// </summary>
        public Action CloseAction { get; set; } 
        #endregion
    }
}