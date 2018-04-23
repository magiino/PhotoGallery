using System.Collections.Generic;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IPersonTagRepository
    {
        PersonListModel GetById(int id);
        ICollection<PersonListModel> GetAll();
        PersonListModel GetByFirstName(string name);
        PersonListModel GetByLastName(string name);

        PersonTagEntity Add(PersonTagEntity person);
        bool Delete(int id);
        bool Update(PersonTagDetailModel person);
    }
}