using System;
using System.Collections.Generic;

namespace apisam.entities.ViewModels
{
    public class CalendarioMovilViewModel
    {
        public CalendarioMovilViewModel()
        {
        }

        public DateTime Date { get; set; }
        public List<CalendarioFecha> Events { get; set; }

    }
}
