using System.Collections.Generic;
using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        PersonModel GetById(int id);
        ICollection<PersonModel> GetAll();
        bool Delete(int id);
    }
}