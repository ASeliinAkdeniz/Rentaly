using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rentaly.DTOLayer.CustomerDtos;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.BusinessLayer.Abstract
{
    public interface ICustomerService
    {
        Task<List<ResultCustomerDto>> TGetListAsync();
        Task<GetCustomerByIdDto> TGetByIdAsync(int id);
        Task TInsertAsync(CreateCustomerDto dto);
        Task TUpdateAsync(UpdateCustomerDto dto);
        Task TDeleteAsync(int id);
    }
    
}
