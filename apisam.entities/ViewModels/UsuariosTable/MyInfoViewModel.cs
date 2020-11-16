using System;
using System.Collections.Generic;
using System.Text;

namespace apisam.entities.ViewModels.UsuariosTable
{
   public  class MyInfoViewModel
    {
        public Planes Plan { get; set; }
        public EditUserViewModel Usuario { get; set; }
        public int ConsultasAtendidas { get; set; }
        public int imagenesConsumidas { get; set; }
    }
}
