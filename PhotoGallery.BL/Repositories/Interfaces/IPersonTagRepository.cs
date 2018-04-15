using System.Collections.Generic;
using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IPersonTagRepository
    {
        ICollection<PersonTagListModel> GetAll();
        PersonTagListModel GetByFirstName(string name);
        PersonTagListModel GetByLastName(string name);
    }
}