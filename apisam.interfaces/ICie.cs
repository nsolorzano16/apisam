using apisam.entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace apisam.interfaces
{
   public  interface ICie
    {
        Task<PageResponse<CIE>> GetEnfermedades(int pageNo, int limit, string filter);
    }
}
