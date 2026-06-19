using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.RepositoryDesignPattern;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.DataAccessLayer.Concrete
{
    public class CarModelRepository : GenericRepository<CarModel>, ICarModelDal
    {
        public CarModelRepository(RentalyContext context) : base(context) { }
    }
}
