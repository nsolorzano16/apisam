using apisam.entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace apisam.interfaces
{
    public interface IPlanes
    {
        Task<RespuestaMetodos> AddPlan(Planes plan);
        Task<RespuestaMetodos> UpdatePlan(Planes plan);
        Task<Planes> GetPlanById(int id);

        Task <List<Planes>> GetPlanes();




    }
}
