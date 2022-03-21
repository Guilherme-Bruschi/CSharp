using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class UsuarioDAO
    {
        public int ValidarLogin(string email, string senha)
        {
            banco objbanco = new banco();

            tb_usuario objUser = objbanco.tb_usuario.Where(us => us.email_usuario == email && us.senha_usuario == senha).FirstOrDefault();

            if (objUser == null)
            {
                return -1;
            }
            else
            {
                return objUser.id_usuario;

            }
        }
    }
}
