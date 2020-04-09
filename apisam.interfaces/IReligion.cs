using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IReligion
    {
        List<Religion> Religiones { get; }
        RespuestaMetodos Add(Religion religion);
        RespuestaMetodos Update(Religion religion);
        Religion GetReligionById(int id);
    }
}
