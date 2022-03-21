using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class ContaDAO
    {
        public void CadastrarConta(tb_conta objConta)
        {
            banco objbanco = new banco();

            objbanco.AddTotb_conta(objConta);

            objbanco.SaveChanges();
        }

        public List<tb_conta> ConsultarContas (int codUsuario)
        {
            banco objbanco = new banco();

            List<tb_conta> lstcontas = objbanco.tb_conta.Where(cont => cont.id_usuario == codUsuario).ToList();

            return lstcontas;
        }
        public void AlterarConta(tb_conta objConta)
        {
            //Instancia o banco
            banco objbanco = new banco();

            //Recupera o obj que deve ser alterado
            tb_conta objContaRecuperada = objbanco.tb_conta.Where(cont => cont.id_conta == objConta.id_conta).FirstOrDefault();

            //Atualiza as alterações
            objContaRecuperada.nome_conta = objConta.nome_conta;
            objContaRecuperada.numero_conta = objConta.numero_conta;
            objContaRecuperada.agencia_conta = objConta.agencia_conta;
            objContaRecuperada.saldo_conta = objConta.saldo_conta;

            //Salva no banco
            objbanco.SaveChanges();

        }
        public void ExcluirConta(int idConta)
        {
            banco objbanco = new banco();

            tb_conta objContaExcluir = objbanco.tb_conta.Where(cont => cont.id_conta == idConta).FirstOrDefault();

            objbanco.DeleteObject(objContaExcluir);

            objbanco.SaveChanges();

        }
    }
}
