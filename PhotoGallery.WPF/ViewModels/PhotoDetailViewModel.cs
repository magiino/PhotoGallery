using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PhotoGallery.BL;
using PhotoGallery.BL.MessengerFile.Messeges;
using PhotoGallery.BL.Models;
using PhotoGallery.WPF.ViewModels.Base;

namespace PhotoGallery.WPF.ViewModels
{
    public class PhotoDetailViewModel : BaseViewModel
    {
    private readonly IMessenger _messenger;
    private readonly IUnitOfWork _unitOfWork;

    private PhotoDetailModel _photo;
    public string PhotoPath { get; set; }
    public string PhotoName { get; set; }
    public ObservableCollection<TagModel> Tags { get; set; }

    public List<int> PhotosIds { get; set; }
    public int CurrentPhotoIndex { get; set; }
    public int NumOfPhotos { get; set; }

    public ICommand PreviousPhoto { get; set; }
    public ICommand NextPhoto { get; set; }


    // TODO oznacovanie tagov na fotke
    public PhotoDetailViewModel(IMessenger messenger, IUnitOfWork unitOfWork)
    {
        _messenger = messenger;
        _unitOfWork = unitOfWork;

        PreviousPhoto = new RelayCommand(GetPreviousPhoto, GetPreviousPhotosCanUse);
        NextPhoto = new RelayCommand(GetNextPhoto, GetNextPhotosCanUse);

        _messenger.Register<SendFilterWithPhoto>(OnLoad);
        _messenger.Register<SendNewTag>(msg => Tags.Add(msg.TagModel));
        _messenger.Register<SendNewPhotoName>(msg => PhotoName = msg.PhotoName);
    }

    private void GetPreviousPhoto()
    {
        _photo = _unitOfWork.Photos.GetDetailModelById(PhotosIds[++CurrentPhotoIndex]);
        SendPhotoToDetailVm(_photo);
        SetPhotoProperties(_photo);
    }

    private void GetNextPhoto()
    {
        _photo = _unitOfWork.Photos.GetDetailModelById(PhotosIds[--CurrentPhotoIndex]);
        SendPhotoToDetailVm(_photo);
        SetPhotoProperties(_photo);
    }

    public bool GetNextPhotosCanUse()
    {
        return CurrentPhotoIndex <= NumOfPhotos;
    }

    public bool GetPreviousPhotosCanUse()
    {
        return CurrentPhotoIndex >= 0;
    }

    private void OnLoad(SendFilterWithPhoto filterAndPhoto)
    {
        _photo = _unitOfWork.Photos.GetDetailModelById(filterAndPhoto.PhotoId);
        SendPhotoToDetailVm(_photo);
        SetPhotoProperties(_photo);
        // fetch filtered and sorted data which are from correct album or contains good tag
        PhotosIds = _unitOfWork.Photos.GetSortedFilteredPhotosIds(filterAndPhoto.FilterSortSettings,
            filterAndPhoto.ChosenItem);
        // Zistenie aktualneho indexu
        CurrentPhotoIndex = PhotosIds.IndexOf(_photo.Id);
        // Zistenie poctu fotiek
        NumOfPhotos = PhotosIds.Count;
    }

    private void SetPhotoProperties(PhotoDetailModel photoDetailModel)
    {
        PhotoPath = photoDetailModel.Path;
        PhotoName = PhotoName;
        Tags = new ObservableCollection<TagModel>(photoDetailModel.Tags);
    }

    private void SendPhotoToDetailVm(PhotoDetailModel photoDetail)
    {
        _messenger.Send(new SendDetailPhotoModel(photoDetail));
    }
    }
}