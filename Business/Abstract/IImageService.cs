using ProductTrackingSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IImageService
    {
        List<Image> GetAll();
        void Add(Image image);
        Image GetById(int imageId);
        void Delete(Image image);
        void Update(Image image);
    }
}
