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
    public class AwardManager : IAwardService
    {
        private readonly IAwardDal _awardDal;
        public AwardManager(IAwardDal awardDal) { _awardDal = awardDal; }

        public async Task TDeleteAsync(int id) => await _awardDal.DeleteAsync(id);
        public async Task<Award> TGetByIdAsync(int id) => await _awardDal.GetByIdAsync(id);
        public async Task<List<Award>> TGetListAsync() => await _awardDal.GetListAsync();
        public async Task TInsertAsync(Award entity) => await _awardDal.InsertAsync(entity);
        public async Task TUpdateAsync(Award entity) => await _awardDal.UpdateAsync(entity);
    }
}
