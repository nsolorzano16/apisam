using System;
using System.Collections.Generic;

namespace apisam.entities.ViewModels
{
    public class ConsultaViewModel
    {
        public ConsultaViewModel()
        {
        }

        public Preclinica Preclinica { get; set; }
        public AntecedentesFamiliaresPersonales AntecedentesFamiliaresPersonales { get; set; }
        public Habitos Habitos { get; set; }
        public HistorialGinecoObstetra HistorialGinecoObstetra { get; set; }
        public List<FarmacosUsoActual> FarmacosUsoActual { get; set; }
        public ExamenFisico ExamenFisico { get; set; }
        public ExamenFisicoGinecologico ExamenFisicoGinecologico { get; set; }
        public List<Diagnosticos> Diagnosticos { get; set; }
        public List<Notas> Notas { get; set; }
        public ConsultaGeneral ConsultaGeneral { get; set; }
        public List<ExamenesIndicadosViewModel> ExamenesIndicados { get; set; }
        public PlanTerapeutico PlanTerapeutico { get; set; }



    }
}
