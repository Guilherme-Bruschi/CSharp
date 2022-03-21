using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class CadastroDAO
    {
        public void CadastrarUsuario(tb_usuario objUsuario)
        {
            banco objbanco = new banco();

            objbanco.AddTotb_usuario(objUsuario);

            objbanco.SaveChanges();
        }
    }
}
