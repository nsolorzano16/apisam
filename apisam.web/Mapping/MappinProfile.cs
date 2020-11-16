using System;
using System.Collections.Generic;
using apisam.entities;
using apisam.entities.ViewModels;
using apisam.entities.ViewModels.UsuariosTable;
using apisam.web.Data;
//using apisam.entities.ViewModels.UsuariosTable;
using AutoMapper;
using iText.Layout.Element;

namespace apisam.web.Mapping
{
    public class MappinProfile : Profile
    {
        public MappinProfile()
        {
            // CreateMap<UsuarioEditarViewModel, Usuario>();
            CreateMap<PacientesViewModel, Paciente>();
            CreateMap<PreclinicaViewModel, Preclinica>();
            CreateMap<ExamenesIndicadosViewModel, ExamenIndicado>();
            CreateMap<PlanTerapeuticoViewModel, PlanTerapeutico>();
            CreateMap<DiagnosticosViewModel, Diagnosticos>();
            CreateMap<CreateUserViewModel, User>();
            CreateMap<EditUserViewModel, User>();
            CreateMap<User, EditUserViewModel>();
          //  CreateMap<List<DiagnosticosViewModel>, List<Diagnosticos>>();
        }
    }
}
