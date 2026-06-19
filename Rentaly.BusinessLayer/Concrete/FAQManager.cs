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
    public class FAQManager : IFAQService
    {
        private readonly IFAQDal _faqDal;
        public FAQManager(IFAQDal faqDal) { _faqDal = faqDal; }

        public async Task TDeleteAsync(int id) => await _faqDal.DeleteAsync(id);
        public async Task<FAQ> TGetByIdAsync(int id) => await _faqDal.GetByIdAsync(id);
        public async Task<List<FAQ>> TGetListAsync() => await _faqDal.GetListAsync();
        public async Task TInsertAsync(FAQ entity) => await _faqDal.InsertAsync(entity);
        public async Task TUpdateAsync(FAQ entity) => await _faqDal.UpdateAsync(entity);
    }
}
