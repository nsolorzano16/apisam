using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IAnticonceptivos
    {
        Task<List<Anticonceptivos>> GetAnticonceptivos();
    }
}
