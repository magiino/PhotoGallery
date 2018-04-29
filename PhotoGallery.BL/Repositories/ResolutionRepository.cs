using System.Collections.Generic;
using System.Linq;
using PhotoGallery.DAL;
using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.Repositories
{
    public class ResolutionRepository //: IResolutionRepository
    {
        private readonly DataContext _dataContext;

        public ResolutionRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ResolutionModel GetById(int id)
        {
            var resolution = _dataContext.Resolutions.FirstOrDefault(x => x.Id == id);
            return Mapper.ResolutionEntityToResolutionModel(resolution);
        }

        public ICollection<ResolutionModel> GetAll()
        {
            return Mapper.ResolutionEntitiesToResolutionModels(_dataContext.Resolutions.ToList());
        }

        public ResolutionModel Add(ResolutionModel resolutionModel)
        {
            var resolution = _dataContext.Resolutions.Add(Mapper.ResolutionModelToResolutionEntity(resolutionModel));
            _dataContext.SaveChanges();
            return Mapper.ResolutionEntityToResolutionModel(resolution);
        }

        public ResolutionModel GetByWidthAndHeight(int height, int width)
        {
            var resolution = _dataContext.Resolutions.ToList().SingleOrDefault(x => x.Resolution == $"{height} {width}");
            return resolution == null ? null : Mapper.ResolutionEntityToResolutionModel(resolution);
        }
    }
}