using Microsoft.EntityFrameworkCore;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.DataAccessLayer.Concrete
{
    public class RentalyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-BMACQ1D\\SQLEXPRESS;Database=RentalyDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        // Yeni eklenenler
        public DbSet<Process> Processes { get; set; }
        public DbSet<OurFuture> OurFutures { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
    }
}
