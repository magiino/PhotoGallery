using System.Collections.Generic;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IPersonTagRepository
    {
        PersonTagListModel GetById(int id);
        ICollection<PersonTagListModel> GetAll();
        PersonTagListModel GetByFirstName(string name);
        PersonTagListModel GetByLastName(string name);

        PersonTagEntity Add(PersonTagEntity person);
        bool Delete(int id);
        bool Update(PersonTagDetailModel person);
    }
}