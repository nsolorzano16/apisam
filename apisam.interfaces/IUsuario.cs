namespace apisam.interfaces
{
    using apisam.entities;
    using apisam.entities.ViewModels.UsuariosTable;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUsuario
    {
 

        List<AspNetRoles> Roles { get; }

        Task<EditUserViewModel> GetMyInfo(string id);

        Task<PageResponse<EditUserViewModel>> GetAsistentes(int pageNo, int limit, string filter, string doctorId);

        Task<PageResponse<EditUserViewModel>> GetUsuarios(int pageNo, int limit, string filter);
    }
}
