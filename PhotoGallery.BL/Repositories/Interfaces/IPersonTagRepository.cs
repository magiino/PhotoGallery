using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IPersonTagRepository
    {
        //PersonTagModel GetById(int id);
        //ICollection<PersonTagModel> GetAll();
        //PersonTagModel GetByFirstName(string name);
        //PersonTagModel GetByLastName(string name);

        int Add(TagModel item, PhotoDetailModel photo);
        bool Delete(int id);
        //bool Update(PersonTagModel person);
    }
}