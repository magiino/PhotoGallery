using System.Collections.Generic;
using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IPersonTagRepository
    {
        ICollection<PersonTagModel> GetAll();
        PersonTagModel GetByFirstName(string name);
        PersonTagModel GetByLastName(string name);
    }
}