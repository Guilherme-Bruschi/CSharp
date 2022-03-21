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
    public partial class frmEmpresa : Form
    {
        public frmEmpresa()
        {
            InitializeComponent();
        }

        private void frmEmpresa_Load(object sender, EventArgs e)
        {
            EstadoInicial();
            LimparCampos();
            CarregarGrid();
        }

        private void CarregarGrid()
        {
            //Instancia o DAO
            EmpresaDAO objDAO = new EmpresaDAO();

            //Cria a lista a ser consultada
            List<tb_empresa> lstempresas = objDAO.ConsultarEmpresas(Usuario.CodigoLogado);

            //Carrega a grid
            grdEmpresas.DataSource = lstempresas;

            //Oculta colunas indesejadas
            grdEmpresas.Columns["id_usuario"].Visible = false;
            grdEmpresas.Columns["id_empresa"].Visible = false;
            grdEmpresas.Columns["tb_movimento"].Visible = false;
            grdEmpresas.Columns["tb_usuario"].Visible = false;
            grdEmpresas.Columns["nome_empresa"].HeaderText = "Empresas";
            grdEmpresas.Columns["telefone_empresa"].HeaderText = "Telefones";
            grdEmpresas.Columns["endereco_empresa"].HeaderText = "Endereços";

        }

        private void EstadoInicial()
        {
            btnSalvar.Text = "Cadastrar";
            btnExcluir.Visible = false;
        }
        private void LimparCampos()
        {
            txtEndereco.Clear();
            txtNome.Clear();
            txtTel.Clear();
        }

        private bool ValidarCampos()
        {
            bool ret = true;
            string campos = "";

            if(txtNome.Text.Trim() == "")
            {
                ret = false;
                campos = "- Nome da Empresa\n";
            }
            if(txtEndereco.Text.Trim() == "")
            {
                ret = false;
                campos += "- Endereço\n";
            }
            if (txtTel.Text.Trim() == "")
            {
                ret = false;
                campos += "- Telefone\n";
            }

            if(!ret)
            {
                MessageBox.Show("Preencher o(s) campo(s):\n" + campos, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return ret;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if(ValidarCampos())
            {
                //Instancia o objeto para poder preencher as informações de acordo com a tela.
                tb_empresa objEmpresa = new tb_empresa();

                //Instancia para chamar as operações e gravar no banco.
                EmpresaDAO objDAO = new EmpresaDAO();

                //Preenche as propriedades de acordo com a tela
                objEmpresa.nome_empresa = txtNome.Text.Trim();
                objEmpresa.endereco_empresa = txtEndereco.Text.Trim();
                objEmpresa.telefone_empresa = txtTel.Text.Trim();
                objEmpresa.id_usuario = Usuario.CodigoLogado;

                try
                {
                    if(txtCodigo.Text == "")
                    {
                        //Chama a operação que cadastrará a categoria
                        objDAO.CadastrarEmpresa(objEmpresa);
                    }
                    else
                    {
                        objEmpresa.id_empresa = Convert.ToInt32(txtCodigo.Text);
                        objDAO.AlterarEmpresa(objEmpresa);
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
            EstadoInicial();
        }

        private void grdEmpresas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Verifica se há linhas na grid
            if(grdEmpresas.RowCount > 0)
            {
                //Recupera linha clicada
                tb_empresa objLinhaClicada = (tb_empresa)grdEmpresas.CurrentRow.DataBoundItem;

                //Preencher os campos
                txtCodigo.Text = Convert.ToString(objLinhaClicada.id_empresa);
                txtNome.Text = objLinhaClicada.nome_empresa;
                txtEndereco.Text = objLinhaClicada.endereco_empresa;
                txtTel.Text = objLinhaClicada.telefone_empresa;

                btnExcluir.Visible = true;
                btnSalvar.Text = "Alterar";

            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir a empresa?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int idEmpresa = Convert.ToInt32(txtCodigo.Text);
                EmpresaDAO objDAO = new EmpresaDAO();

                try
                {
                    objDAO.ExcluirEmpresa(idEmpresa);
                    LimparCampos();
                    EstadoInicial();
                    CarregarGrid();
                    MessageBox.Show("Empresa excluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("A Empresa não poderá ser excluída pois está em uso!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
