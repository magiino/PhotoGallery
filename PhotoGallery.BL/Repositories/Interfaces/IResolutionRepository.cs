using System.Collections.Generic;
using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IResolutionRepository
    {
        ResolutionModel GetById(int id);
        ICollection<ResolutionModel> GetAll();
        ResolutionModel Add(ResolutionModel resolutionModel);
        ResolutionModel GetByWidthAndHeight(int height, int width);
    }
}