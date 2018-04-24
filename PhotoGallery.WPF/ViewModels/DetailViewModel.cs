using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using PhotoGallery.BL;
using PhotoGallery.BL.MessengerFile.Messeges;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Enums;

namespace PhotoGallery.WPF.ViewModels
{
    public class DetailViewModel
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

        public ICommand AddTagCommand { get; }
        public ICommand DeleteTagCommand { get; }
        public ICommand SaveChangesCommand { get; }

        public DetailViewModel(IMessenger messenger, IUnitOfWork unitOfWork)
        {
            _messenger = messenger;
            _unitOfWork = unitOfWork;

            AddTagCommand = new RelayCommand(AddTag);
            DeleteTagCommand = new RelayCommand(DeleteTag, DeleteTagCanUse);
            SaveChangesCommand = new RelayCommand(SaveChanges);

            _messenger.Register<SendDetailPhotoModel>(PhotoDetailChanged);
        }

        private void SaveChanges()
        {
            _unitOfWork.Photos.Update(new PhotoDetailModel()
            {
                Id = _id,
                Name = Name,
                Location = Location,
                Note = Note,
                Tags = Tags.ToList(),
            });
        }

        private void DeleteTag()
        {
            if (SelectedTagModel.IsItem)
                _unitOfWork.ItemTags.Delete(SelectedTagModel.Id);
            else
                _unitOfWork.PersonTags.Delete(SelectedTagModel.Id);

            Tags.Remove(SelectedTagModel);
            SelectedTagModel = null;
        }

        private void AddTag()
        {
            var r = new Random();
            TagModel tag;

            if (r.Next(50, 150) > 100)
            {
                tag = new TagModel()
                {
                    IsItem = true,
                    Name = "Okno",
                    XPosition = r.Next(100, 300),
                    YPosition = r.Next(100, 300),
                };

                tag.Id = _unitOfWork.ItemTags.Add(tag, _photoDetailModel);
            }
            else
            {
                tag = new TagModel()
                {
                    Name = "Jozef Ale",
                    XPosition = r.Next(100, 300),
                    YPosition = r.Next(100, 300),
                };
                tag.Id = _unitOfWork.PersonTags.Add(tag, _photoDetailModel);
            }

            Tags.Add(tag);
            // TODO Pridat tag do detail VM
            // TODO pridat novu osobu/item do leftVM
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