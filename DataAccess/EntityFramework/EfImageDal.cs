using DataAccess.Abstract;
using DataAccess.Concrete.Repositories;
using ProductTrackingSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework
{
    public class EfImageDal : GenericRepository<Image>, IImageDal
    {
    }
}
