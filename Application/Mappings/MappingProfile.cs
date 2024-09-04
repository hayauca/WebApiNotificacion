using Application.DTOs;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           
            CreateMap<NotificacionesCliente, NotificacionesClienteDto>().ReverseMap();
            CreateMap<NotificacionesClienteConfig, NotificacionesClienteConfigDto>().ReverseMap();
        }

    }
}
