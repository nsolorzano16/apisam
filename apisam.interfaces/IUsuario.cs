using System;
using System.Collections.Generic;
using apisam.entities;
using apisam.entities.ViewModels;
using apisam.entities.ViewModels.UsuariosTable;

namespace apisam.interfaces
{
    public interface IUsuario
    {

        List<Usuario> Usuarios { get; }
        List<Rol> Roles { get; }
        RespuestaMetodos AddUsuario(Usuario usuario);
        RespuestaMetodos UpdateUsuario(Usuario usuario);
        Usuario GetUsuarioByUserName(LoginViewModel usuario);
        Usuario GerUserById(int id);
        PageResponse<Usuario>
            GetAsistentes(int pageNo, int limit, string filter, int doctorId);
        Usuario UpdatePassword(UserChangePassword model);

    }
}
