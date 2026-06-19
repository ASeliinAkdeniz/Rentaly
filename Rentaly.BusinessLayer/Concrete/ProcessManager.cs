using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;

namespace Rentaly.BusinessLayer.Concrete
{
    public class ProcessManager : IProcessService
    {
        private readonly IProcessDal _processDal;

        public ProcessManager(IProcessDal processDal)
        {
            _processDal = processDal;
        }

        public async Task TDeleteAsync(int id) => await _processDal.DeleteAsync(id);
        public async Task<Rentaly.EntityLayer.Entities.Process> TGetByIdAsync(int id) => await _processDal.GetByIdAsync(id);
        public async Task<List<Rentaly.EntityLayer.Entities.Process>> TGetListAsync() => await _processDal.GetListAsync();
        public async Task TInsertAsync(Rentaly.EntityLayer.Entities.Process entity) => await _processDal.InsertAsync(entity);
        public async Task TUpdateAsync(Rentaly.EntityLayer.Entities.Process entity) => await _processDal.UpdateAsync(entity);
    }
}