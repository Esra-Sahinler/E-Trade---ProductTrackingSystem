using Business.Abstract;
using DataAccess.Abstract;
using ProductTrackingSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ImageManager : IImageService
    {
        IImageDal _imageDal;
        public ImageManager(IImageDal imageDal)
        {
            _imageDal = imageDal;
        }
        public void Add(Image image)
        {
            _imageDal.Add(image);
        }

        public void Delete(Image image)
        {
            _imageDal.Delete(image);
        }

        public List<Image> GetAll()
        {
            return _imageDal.GetAll();
        }

        public Image GetById(int imageId)
        {
            return _imageDal.Get(i => i.ImageId == imageId);
        }

        public void Update(Image image)
        {
            _imageDal.Update(image);
        }
    }
}
