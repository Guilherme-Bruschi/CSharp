using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DAO
{
    public class MovimentoDAO
    {
        public void RealizarMovimento(tb_movimento objMOV)
        {
            banco objbanco = new banco();

            objbanco.AddTotb_movimento(objMOV);


            using (TransactionScope Trans = new TransactionScope())
            {
                //Inserção no banco da tb_movimento
                objbanco.SaveChanges();

                //Resgata a conta para ser atualizada
                tb_conta objResgate = objbanco.tb_conta.Where(cont => cont.id_conta == objMOV.id_conta).FirstOrDefault();

                //Verifica se é uma entrada
                if(objMOV.tipo_movimento == 0)
                {
                    objResgate.saldo_conta = objResgate.saldo_conta + objMOV.valor_movimento;
                }
                else //Saida
                {
                    objResgate.saldo_conta = objResgate.saldo_conta - objMOV.valor_movimento;
                }

                //Atualizar o saldo na tb_conta
                objbanco.SaveChanges();

                Trans.Complete();
            }

        }
        public List<MovimentoVO> PesquisarMovimento(int tipo, DateTime dtInicial, DateTime dtFinal, int codLogado)
        {
            banco objBanco = new banco();

            List<tb_movimento> lstConsulta = new List<tb_movimento>();
            List<MovimentoVO> lstRetorno = new List<MovimentoVO>();

            if(tipo != 2)
            {
                lstConsulta = objBanco.tb_movimento.Include("tb_categoria").Include("tb_empresa").Include("tb_conta").Where(mov => mov.id_usuario == codLogado && mov.data_movimento >= dtInicial && mov.data_movimento <= dtFinal && mov.tipo_movimento == tipo).ToList();
            }
            else
            {
                lstConsulta = objBanco.tb_movimento.Include("tb_categoria").Include("tb_empresa").Include("tb_conta").Where(mov => mov.id_usuario == codLogado && mov.data_movimento >= dtInicial && mov.data_movimento <= dtFinal).ToList();
            }

            for (int i = 0; i < lstConsulta.Count; i++)
            {
                MovimentoVO objVO = new MovimentoVO();

                objVO.Data = lstConsulta[i].data_movimento.ToShortDateString();
                objVO.Valor = lstConsulta[i].valor_movimento;
                objVO.Obs = lstConsulta[i].obs_movimento;
                objVO.Categoria = lstConsulta[i].tb_categoria.nome_categoria;
                objVO.Empresa = lstConsulta[i].tb_empresa.nome_empresa;
                objVO.Conta = lstConsulta[i].tb_conta.nome_conta + " / " + "Num.:" + lstConsulta[i].tb_conta.numero_conta;
                objVO.Tipo = lstConsulta[i].tipo_movimento == 0 ? "Entrada" : "Saída";

                lstRetorno.Add(objVO);
            }
            return lstRetorno;


        }
    }
}
