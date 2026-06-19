using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.BusinessLayer.Concrete
{
    public class TestimonialManager : ITestimonialService
    {
        private readonly ITestimonialDal _testimonialDal;
        public TestimonialManager(ITestimonialDal testimonialDal) { _testimonialDal = testimonialDal; }

        public async Task TDeleteAsync(int id) => await _testimonialDal.DeleteAsync(id);
        public async Task<Testimonial> TGetByIdAsync(int id) => await _testimonialDal.GetByIdAsync(id);
        public async Task<List<Testimonial>> TGetListAsync() => await _testimonialDal.GetListAsync();
        public async Task TInsertAsync(Testimonial entity) => await _testimonialDal.InsertAsync(entity);
        public async Task TUpdateAsync(Testimonial entity) => await _testimonialDal.UpdateAsync(entity);
    }
}
