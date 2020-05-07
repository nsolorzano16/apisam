using System;
namespace apisam.entities.ViewModels
{
    public class ExamenesIndicadosViewModel : ExamenIndicado
    {
        public ExamenesIndicadosViewModel()
        {
        }

        public string ExamenCategoria { get; set; }
        public string ExamenTipo { get; set; }
        public string ExamenDetalle { get; set; }
    }
}
