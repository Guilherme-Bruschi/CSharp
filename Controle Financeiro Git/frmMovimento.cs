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
    public partial class frmMovimento : Form
    {
        public frmMovimento()
        {
            InitializeComponent();
        }


        private void frmMovimento_Load(object sender, EventArgs e)
        {
            EstadoInicial();
            LimparCampos();
            CarregarCategorias();
            CarregarEmpresas();
            CarregarContas();
            cbTipoMovimento.SelectedIndex = 2;

        }

        private void EstadoInicial()
        {
            btnSalvar.Text = "Cadastrar";
            btnExcluir.Visible = false;
        }
        private void LimparCampos()
        {
            txtCodigo.Clear();
            txtObs.Clear();
            txtValor.Clear();
            cbCategoria.SelectedIndex = -1;
            cbConta.SelectedIndex = -1;
            cbEmpresa.SelectedIndex = -1;
            cbMovimento.SelectedIndex = -1;
            cbTipoMovimento.SelectedIndex = -1;
        }
        private bool ValidarCampos()
        {
            bool ret = true;
            string campos = "";

            if(cbMovimento.SelectedIndex == -1)
            {
                ret = false;
                campos = "- Tipo do Movimento\n";
            }
            if (cbCategoria.SelectedIndex == -1)
            {
                ret = false;
                campos += "- Categoria\n";
            }
            if (cbConta.SelectedIndex == -1 )
            {
                ret = false;
                campos += "- Conta\n";
            }
            if (cbEmpresa.SelectedIndex == -1)
            {
                ret = false;
                campos += "- Empresa\n";
            }
            if (txtValor.Text.Trim() == "")
            {
                ret = false;
                campos += "- Valor";
            }
            if (!ret)
            {
                MessageBox.Show("Preencha o(s) campo(s):\n" + campos, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return ret;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if(ValidarCampos())
            {
                tb_movimento objMov = new tb_movimento();
                MovimentoDAO objDAO = new MovimentoDAO();

                objMov.id_usuario = Usuario.CodigoLogado;
                objMov.data_movimento = dtpData.Value;
                objMov.valor_movimento = Convert.ToDecimal(txtValor.Text);
                objMov.tipo_movimento = Convert.ToInt16(cbMovimento.SelectedIndex);
                objMov.id_categoria = Convert.ToInt32(cbCategoria.SelectedValue);
                objMov.id_empresa = Convert.ToInt32(cbEmpresa.SelectedValue);
                objMov.id_conta = Convert.ToInt32(cbConta.SelectedValue);
                objMov.obs_movimento = txtObs.Text.Trim();

                try
                {
                    objDAO.RealizarMovimento(objMov);
                    LimparCampos();
                    MessageBox.Show("Ação realizada com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch 
                {
                    MessageBox.Show("Ocorreu um erro na operação. Tente novamente mais tarde!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
            EstadoInicial();
        }

        private void CarregarCategorias()
        {
            cbCategoria.ValueMember = "id_categoria";
            cbCategoria.DisplayMember = "nome_categoria";

            CategoriaDAO objDAO = new CategoriaDAO();
            List<tb_categoria> lstcategorias = objDAO.ConsultarCategoria(Usuario.CodigoLogado);

            cbCategoria.DataSource = lstcategorias;
        }
        private void CarregarEmpresas()
        {
            cbEmpresa.ValueMember = "id_empresa";
            cbEmpresa.DisplayMember = "nome_empresa";

            EmpresaDAO objDAO = new EmpresaDAO();
            List<tb_empresa> lstempresas = objDAO.ConsultarEmpresas(Usuario.CodigoLogado);

            cbEmpresa.DataSource = lstempresas;
        }
        private void CarregarContas()
        {
            cbConta.ValueMember = "id_conta";
            cbConta.DisplayMember = "numero_conta";

            ContaDAO objDAO = new ContaDAO();
            List<tb_conta> lstcontas = objDAO.ConsultarContas(Usuario.CodigoLogado);

            cbConta.DataSource = lstcontas;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            PesquisarMovimento();
        }
        private void PesquisarMovimento()
        {
            DateTime dtInicial = dtpInicio.Value.Date;
            DateTime dtFinal = dtpFim.Value.Date;
            int tipoSel = cbTipoMovimento.SelectedIndex;

            MovimentoDAO objDAO = new MovimentoDAO();

            List<MovimentoVO> lstMovimento = objDAO.PesquisarMovimento(tipoSel, dtInicial, dtFinal, Usuario.CodigoLogado);

            grdMovimento.DataSource = lstMovimento;
        }

        private void cbTipoMovimento_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
