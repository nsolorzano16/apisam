using System;
using System.Collections.Generic;
using apisam.entities;
using apisam.entities.ViewModels;

namespace apisam.interfaces
{
    public interface IUsuario
    {

        List<Usuario> Usuarios { get; }
        bool AddUsuario(Usuario usuario);
        bool UpdateUsuario(Usuario usuario);
        Usuario GetUsuarioByUserName(LoginViewModel usuario);

    }
}
