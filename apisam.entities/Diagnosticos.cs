﻿using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class Diagnosticos : RegistroBase
    {
        public Diagnosticos()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int DiagnosticoId { get; set; }
        public int PacienteId { get; set; }
        public string DoctorId { get; set; }
        public int CieId { get; set; }
        public int PreclinicaId { get; set; }
        public string ProblemasClinicos { get; set; }


    }
}
