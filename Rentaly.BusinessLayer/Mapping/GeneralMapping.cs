using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Rentaly.DTOLayer.CustomerDtos;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.BusinessLayer.Mapping
{
   public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Customer, CreateCustomerDto>().ReverseMap();
            CreateMap<Customer, ResultCustomerDto>().ReverseMap();
            CreateMap<Customer, GetCustomerByIdDto>().ReverseMap();
            CreateMap<Customer, UpdateCustomerDto>().ReverseMap();
        }
        
    }
}
