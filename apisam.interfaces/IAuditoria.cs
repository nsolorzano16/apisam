using System;
using System.Threading.Tasks;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IAuditoria
    {
        Task<PageResponse<Auditoria>> GetAuditorias(int pageNo, int limit, string filter);
    }
}
