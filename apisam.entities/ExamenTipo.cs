﻿using System;
namespace apisam.entities
{
    public class ExamenTipo
    {
        public ExamenTipo()
        {
        }

        public int ExamenTipoId { get; set; }
        public int ExamenCategoriaId { get; set; }
        public string Nombre { get; set; }


    }
}
