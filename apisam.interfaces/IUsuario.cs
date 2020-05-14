using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;
using apisam.entities.ViewModels;
using apisam.entities.ViewModels.UsuariosTable;

namespace apisam.interfaces
{
    public interface IUsuario
    {

        Task<List<Usuario>> Usuarios();
        List<Rol> Roles { get; }
        Task<RespuestaMetodos> AddUsuario(Usuario usuario);
        Task<RespuestaMetodos> UpdateUsuario(Usuario usuario);
        Task<Usuario> GetUsuarioByUserName(LoginViewModel usuario);
        Task<Usuario> GerUserById(int id);
        Task<PageResponse<Usuario>>
          GetAsistentes(int pageNo, int limit, string filter, int doctorId);
        Task<Usuario> UpdatePassword(UserChangePassword model);

    }
}
