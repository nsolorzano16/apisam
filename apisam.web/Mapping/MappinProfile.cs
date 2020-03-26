using System;
using apisam.entities;
using apisam.entities.ViewModels;
using apisam.entities.ViewModels.UsuariosTable;
using AutoMapper;

namespace apisam.web.Mapping
{
    public class MappinProfile : Profile
    {
        public MappinProfile()
        {
            CreateMap<UsuarioEditarViewModel, Usuario>();
            CreateMap<PacientesViewModel, Paciente>();
        }
    }
}
