using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class EmpresaDAO
    {
        public void CadastrarEmpresa(tb_empresa objEmpresa)
        {
            //Instancia o BANCO
            banco objbanco = new banco();

            //Adiciona o objeto ao banco
            objbanco.AddTotb_empresa(objEmpresa);

            //Salva as mudanças do banco
            objbanco.SaveChanges();
        
        }

        public List<tb_empresa> ConsultarEmpresas(int codUsuario)
        {
            //Instancia o banco
            banco objbanco = new banco();

            //Cria a lista para receber a consulta filtrada pelo usuario
            List<tb_empresa> lstempresas = objbanco.tb_empresa.Where(emp => emp.id_usuario == codUsuario).ToList();

            //Retorna o Usuario
            return lstempresas;

        }

        public void AlterarEmpresa (tb_empresa objEmpresa)
        {
            //Instancia o banco
            banco objbanco = new banco();

            //Recupera o obj que deve ser alterado
            tb_empresa objEmpresaRecuperada = objbanco.tb_empresa.Where(emp => emp.id_empresa == objEmpresa.id_empresa).FirstOrDefault();

            //Atualiza as alterações
            objEmpresaRecuperada.nome_empresa = objEmpresa.nome_empresa;
            objEmpresaRecuperada.telefone_empresa = objEmpresa.telefone_empresa;
            objEmpresaRecuperada.endereco_empresa = objEmpresa.endereco_empresa;

            //Salva no banco
            objbanco.SaveChanges();

        }
        public void ExcluirEmpresa(int idEmpresa)
        {
            banco objbanco = new banco();

            tb_empresa objEmpresaExcluir = objbanco.tb_empresa.Where(emp => emp.id_empresa == idEmpresa).FirstOrDefault();

            objbanco.DeleteObject(objEmpresaExcluir);

            objbanco.SaveChanges();

        }







    }
}
