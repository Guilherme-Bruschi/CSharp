using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAO;

namespace ControleFinanceiro
{
    public partial class frmConta : Form
    {
        public frmConta()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                //Instancia o Objeto para poder preencher as informações de acordo com a tela
                tb_conta objconta = new tb_conta();

                //Instancia para chamar as operações e gravar no banco
                ContaDAO objDAO = new ContaDAO();

                //Preenche as propriedades de acordo com a tela
                objconta.nome_conta = txtNome.Text.Trim();
                objconta.agencia_conta = txtAgencia.Text.Trim();
                objconta.numero_conta = txtNumero.Text.Trim();
                objconta.saldo_conta = Convert.ToInt32(txtSaldo.Text);
                objconta.id_usuario = Usuario.CodigoLogado;

                try
                {
                    if(txtCodigo.Text == "")
                    {
                        //Chama a operação que cadastrará a conta
                        objDAO.CadastrarConta(objconta);
                    }
                    else
                    {
                        objconta.id_conta = Convert.ToInt32(txtCodigo.Text);
                        objDAO.AlterarConta(objconta);
                    }

                    MessageBox.Show("Ação realizada com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EstadoInicial();
                    LimparCampos();
                    CarregarGrid();

                }
                catch
                {
                    MessageBox.Show("Ocorreu um erro, tente novamente mais tarde.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LimparCampos()
        {
            txtNome.Clear();
            txtNumero.Clear();
            txtSaldo.Clear();
            txtAgencia.Clear();
        }
        private void EstadoInicial()
        {
            btnExcluir.Visible = false;
            btnSalvar.Text = "Cadastrar";
        }
        private bool ValidarCampos()
        {
            bool ret = true;
            string campos = "";

            if(txtNome.Text.Trim() == "")
            {
                ret = false;
                campos += "- Nome do banco\n";
            }
            if (txtAgencia.Text.Trim() == "")
            {
                ret = false;
                campos += "- Nome da agência\n";
            }
            if (txtNumero.Text.Trim() == "")
            {
                ret = false;
                campos += "- Número da conta\n";
            }
            if(txtSaldo.Text.Trim() == "")
            {
                ret = false;
                campos += "- Saldo da conta\n";
            }
            if (!ret)
            {
                MessageBox.Show("Preencher o(s) campo(s):\n" + campos, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return ret;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
            EstadoInicial();
        }

        private void frmConta_Load(object sender, EventArgs e)
        {
            EstadoInicial();
            CarregarGrid();
        }
        private void CarregarGrid()
        {
            //Instancia o DAO
            ContaDAO objDAO = new ContaDAO();

            //Cria a lista a ser consultada
            List<tb_conta> lstcontas = objDAO.ConsultarContas(Usuario.CodigoLogado);

            //Carrega a grid
            grdContas.DataSource = lstcontas;

            //Oculta colunas indesejadas
            grdContas.Columns["id_usuario"].Visible = false;
            grdContas.Columns["id_conta"].Visible = false;
            grdContas.Columns["tb_movimento"].Visible = false;
            grdContas.Columns["tb_usuario"].Visible = false;
            grdContas.Columns["nome_conta"].HeaderText = "Bancos";
            grdContas.Columns["numero_conta"].HeaderText = "Números";
            grdContas.Columns["agencia_conta"].HeaderText = "Agências";
            grdContas.Columns["saldo_conta"].HeaderText = "Saldos";

        }

        private void grdCategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(grdContas.RowCount > 0)
            {
                //Recupera linha clicada
                tb_conta objLinhaClicada = (tb_conta)grdContas.CurrentRow.DataBoundItem;

                //Preenche os campos
                txtCodigo.Text = Convert.ToString(objLinhaClicada.id_conta);
                txtNome.Text = objLinhaClicada.nome_conta;
                txtAgencia.Text = objLinhaClicada.agencia_conta;
                txtNumero.Text = objLinhaClicada.numero_conta;

                btnExcluir.Visible = true;
                btnSalvar.Text = "Alterar";
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir a conta?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int idConta = Convert.ToInt32(txtCodigo.Text);
                ContaDAO objDAO = new ContaDAO();

                try
                {
                    objDAO.ExcluirConta(idConta);
                    LimparCampos();
                    EstadoInicial();
                    CarregarGrid();
                    MessageBox.Show("Conta excluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("A Conta não poderá ser excluída pois está em uso!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void grdContas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
