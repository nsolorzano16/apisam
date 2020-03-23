using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IReligion
    {
        List<Religion> Religiones { get; }
        bool Add(Religion religion);
        bool Update(Religion religion);
        Religion GetReligionById(int id);
    }
}
