using System;
using System.Collections.Generic;
using System.Text;

namespace apisam.entities.ViewModels
{
    public class ExpedienteViewModel
    {
        public AntecedentesFamiliaresPersonales AntecedentesFamiliaresPersonales { get; set; }
        public Habitos Habitos { get; set; }
        public HistorialGinecoViewModel HistorialGinecoObstetra { get; set; }
        public List<FarmacosUsoActual> FarmacosUsoActual { get; set; }
        public PacientesViewModel Paciente { get; set; }

        public List <ConsultaExpedienteViewModel> Consultas { get; set; }
    }




    public class ConsultaExpedienteViewModel
    {
        public Preclinica Preclinica { get; set; }
        public ExamenFisico ExamenFisico { get; set; }
        public List<DiagnosticosViewModel> Diagnosticos { get; set; }
        public List<Notas> Notas { get; set; }
        public ConsultaGeneral ConsultaGeneral { get; set; }
        public List<ExamenesIndicadosViewModel> ExamenesIndicados { get; set; }
        public List<PlanTerapeuticoViewModel> PlanesTerapeuticos { get; set; }

    }
}
