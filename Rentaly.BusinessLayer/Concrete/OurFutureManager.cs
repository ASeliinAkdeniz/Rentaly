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
    public class OurFutureManager : IOurFutureService
    {
        private readonly IOurFutureDal _ourFutureDal;
        public OurFutureManager(IOurFutureDal ourFutureDal) { _ourFutureDal = ourFutureDal; }

        public async Task TDeleteAsync(int id) => await _ourFutureDal.DeleteAsync(id);
        public async Task<OurFuture> TGetByIdAsync(int id) => await _ourFutureDal.GetByIdAsync(id);
        public async Task<List<OurFuture>> TGetListAsync() => await _ourFutureDal.GetListAsync();
        public async Task TInsertAsync(OurFuture entity) => await _ourFutureDal.InsertAsync(entity);
        public async Task TUpdateAsync(OurFuture entity) => await _ourFutureDal.UpdateAsync(entity);
    }
}
