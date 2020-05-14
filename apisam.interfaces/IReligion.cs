using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IReligion
    {
        Task<List<Religion>> Religiones();
        Task<RespuestaMetodos> Add(Religion religion);
        Task<RespuestaMetodos> Update(Religion religion);
        Task<Religion> GetReligionById(int id);
    }
}
