using Microsoft.EntityFrameworkCore;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.RepositoryDesignPattern;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.DataAccessLayer.Concrete
{
    public class CarRepository : GenericRepository<Car>, ICarDal
    {
        private readonly RentalyContext _context;

        public CarRepository(RentalyContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Car>> GetAllCarsWithCategory()
        {
            return await _context.Cars
                .Include(c => c.Category)
                .Include(c => c.Branch)
                .ToListAsync();
        }
    }
}