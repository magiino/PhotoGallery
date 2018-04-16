using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IPersonTagRepository
    {
        ICollection<PersonTagListModel> GetAll();
        PersonTagListModel GetByFirstName(string name);
        PersonTagListModel GetByLastName(string name);


        PersonTagListModel GetById(int id);
        PersonTagEntity Add(PersonTagEntity person);
        void Delete(int id);
        void Update(PersonTagListModel person);

        ICollection<PhotoListModel> GetPhotosPredicate(Expression<Func<PersonTagEntity, bool>> predicate, int pageIndex,
            int pageSize = 6);
    }
}