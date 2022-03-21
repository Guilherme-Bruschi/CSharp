using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class CategoriaDAO
    {
        public void CadastrarCategoria(tb_categoria objCategoria)
        {
            //Instancia o objeto
            banco objbanco = new banco();

            //Adiciona na tabela o OBJ para cadastro
            objbanco.AddTotb_categoria(objCategoria);

            //Salva no banco as alterações
            objbanco.SaveChanges();
        }
        public List<tb_categoria> ConsultarCategoria(int codUsuario)
        {
            //Instancia o banco
            banco objbanco = new banco();

            //Cria uma variavel do tipo LISTA de Categorias para receber o resultado da consulta
            List<tb_categoria> lstCategorias = objbanco.tb_categoria.Where(cat => cat.id_usuario == codUsuario).ToList();

            //Retorna o usuário
            return lstCategorias;

        }
        
        public void AlterarCategoria(tb_categoria objCategoria)
        {
            //Instancia o banco
            banco objbanco = new banco();

            //Recupera o obj que deverá ser alterado
            tb_categoria objCategoriaRecuperada = objbanco.tb_categoria.Where(cat => cat.id_categoria == objCategoria.id_categoria).FirstOrDefault();

            //Atualiza as alterações
            objCategoriaRecuperada.nome_categoria = objCategoria.nome_categoria;

            //Salva as alterações no banco
            objbanco.SaveChanges();

        }

        public void ExcluirCategoria(int idCategoria)
        {
            banco objbanco = new banco();

            tb_categoria objCategoriaExcluir = objbanco.tb_categoria.Where(cat => cat.id_categoria == idCategoria).FirstOrDefault();

            objbanco.DeleteObject(objCategoriaExcluir);

            objbanco.SaveChanges();

        }

    }

    
}
