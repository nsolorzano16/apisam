using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class ExamenFisico : RegistroBase
    {
        public ExamenFisico()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int ExamenFisicoId { get; set; }
        public int PacienteId { get; set; }
        public int DoctorId { get; set; }
        public string AspectoGeneral { get; set; }
        public int? EdadAparente { get; set; }
        public string Marcha { get; set; }
        public string Orientaciones { get; set; }
        public string Pulso { get; set; }
        public string Pabd { get; set; }
        public string Ptorax { get; set; }
        public string Observaciones { get; set; }
        public bool? DolorAusente { get; set; }
        public bool? DolorPresente { get; set; }
        public bool? DolorPresenteLeve { get; set; }
        public bool? DolorPresenteModerado { get; set; }
        public bool? DolorPresenteSevero { get; set; }
        public Double? Imc { get; set; }
        public int? PesoIdeal { get; set; }
        public string Interpretacion { get; set; }
        public bool? ExcesoDePeso { get; set; }
        public int? LibrasABajar { get; set; }
        public string Cabeza { get; set; }
        public string Oidos { get; set; }
        public string Ojos { get; set; }
        public string Fo { get; set; }
        public string Nariz { get; set; }
        public string Orofaringe { get; set; }
        public string Cuello { get; set; }
        public string Torax { get; set; }
        public string Mamas { get; set; }
        public string Pulmones { get; set; }
        public string Corazon { get; set; }
        public string Rot { get; set; }
        public string Abdomen { get; set; }
        public string Pielfoneras { get; set; }
        public string Genitales { get; set; }
        public string RectoProstatico { get; set; }
        public string Miembros { get; set; }
        public string Neurologico { get; set; }



    }
}
