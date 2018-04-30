using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using PhotoGallery.BL;
using PhotoGallery.BL.MessengerFile.Messeges;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Enums;
using PhotoGallery.WPF.ViewModels.Base;

namespace PhotoGallery.WPF.ViewModels
{
    public class InfoViewModel : BaseViewModel
    {
        private readonly IMessenger _messenger;
        private readonly IUnitOfWork _unitOfWork;

        private PhotoDetailModel _photoDetailModel;
        private int _id;
        public string Name { get; set; }
        public DateTime CreatedTime { get; set; }
        public Format Format { get; set; }
        public ResolutionModel Resolution { get; set; }
        public string Note { get; set; }
        public string Location { get; set; }
        public ObservableCollection<TagModel> Tags { get; set; }
        public TagModel SelectedTagModel { get; set; }

        public ICommand DeleteTagCommand { get; }
        public ICommand SaveChangesCommand { get; }

        public InfoViewModel(IMessenger messenger, IUnitOfWork unitOfWork)
        {
            _messenger = messenger;
            _unitOfWork = unitOfWork;

            DeleteTagCommand = new RelayCommand(DeleteTag, DeleteTagCanUse);
            SaveChangesCommand = new RelayCommand(SaveChanges, SaveChangesCanUse);

            _messenger.Register<SendDetailPhotoModel>(PhotoDetailChanged);
            _messenger.Register<SendNewTag>(msg => Tags.Add(msg.TagModel));
        }

        private void SaveChanges()
        {
            var photo = new PhotoDetailModel()
            {
                Id = _id,
                Name = Name,
                Location = Location,
                Note = Note,
                Tags = Tags.ToList(),
            };
            _unitOfWork.Photos.Update(photo);

            if(_photoDetailModel.Name != Name)
                _messenger.Send(new SendNewPhotoName(Name));
            _photoDetailModel = photo;
        }

        public bool SaveChangesCanUse()
        {
            return _photoDetailModel.Name != Name || _photoDetailModel.Note != Note;
        }

        private void DeleteTag()
        {
            // TODO oznamit dalsim VM
            if (SelectedTagModel.IsItem)
                _unitOfWork.ItemTags.Delete(SelectedTagModel.Id);
            else
                _unitOfWork.PersonTags.Delete(SelectedTagModel.Id);

            Tags.Remove(SelectedTagModel);
            SelectedTagModel = null;
        }

        public bool DeleteTagCanUse()
        {
            return SelectedTagModel != null;
        }

        private void PhotoDetailChanged(SendDetailPhotoModel detailPhotoModel)
        {
            var model = _photoDetailModel = detailPhotoModel.PhotoDetailModel;
            _id = model.Id;
            Name = model.Name;
            CreatedTime = model.CreatedTime;
            Format = model.Format;
            Resolution = model.Resolution;
            Location = model.Location;
            Note = model.Note;
            Tags = new ObservableCollection<TagModel>(model.Tags);
        }
    }
}