using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace objCrud
{
    public partial class frmEx3 : Form
    {
        public frmEx3()
        {
            InitializeComponent();
        }

        List<UsuarioVO2> lstUsuarioVO = new List<UsuarioVO2>();

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            double peso, altura;
            

            peso = Convert.ToDouble(txtPeso.Text);
            altura = Convert.ToDouble(txtAltura.Text);
            UsuarioVO2 objRecebe = PrepararObj(peso, altura);
            AdicionarGrid(objRecebe);
            CarregarGrid();
            LimparCampos();

        }

        private UsuarioVO2 PrepararObj(double peso, double altura)
        {
            string situacao;
            double res;
            CalculoIMC objIMC = new CalculoIMC();
            res = objIMC.CalcIMC(peso, altura);
            situacao = objIMC.Situacao(res);
            UsuarioVO2 objRecebe = RetornaUsuario(peso, altura, situacao);

            return objRecebe;
        }

        private void frmEx3_Load(object sender, EventArgs e)
        {
            EstadoInicial();
        }
        private void EstadoInicial()
        {
            btnCalcular.Visible = false;
            btnAlterar.Visible = false;
            btnExcluir.Visible = false;
            txtAltura.Visible = false;
            txtPeso.Visible = false;
            btnCancelar.Visible = false;
            grdAcademia.Visible = false;
            lblPeso.Visible = false;
            lblAltura.Visible = false;
        }
        private void EstadoInicial2()
        {
            btnCalcular.Visible = true;
            btnAlterar.Visible = false;
            btnExcluir.Visible = false;
            btnCancelar.Visible = true;
            txtAltura.Visible = true;
            txtPeso.Visible = true;
            txtLogin.Visible = false;
            txtSenha.Visible = false;
            btnLogin.Visible = false;
            grdAcademia.Visible = true;
            lblLogin.Visible = false;
            lblSenha.Visible = false;
            lblPeso.Visible = true;
            lblAltura.Visible = true;
        }
        private UsuarioVO2 RetornaUsuario (double peso, double altura, string situacao)
        {
            UsuarioVO2 objUsuario = new UsuarioVO2();

            objUsuario.Peso = peso;
            objUsuario.Altura = altura;
            objUsuario.Situacao = situacao;

            return objUsuario;
        }
        private void CarregarGrid()
        {
            grdAcademia.DataSource = null;
            grdAcademia.DataSource = lstUsuarioVO;
        }
        private void AdicionarGrid(UsuarioVO2 objUsuario)
        {
            lstUsuarioVO.Add(objUsuario);
        }
        private void LimparCampos()
        {
            txtAltura.Clear();
            txtPeso.Clear();
        }

        private void grdAcademia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdAcademia.RowCount > 0)
            {
                UsuarioVO2 objLinha = (UsuarioVO2)grdAcademia.CurrentRow.DataBoundItem;
                txtCod.Text = e.RowIndex.ToString();
                txtPeso.Text = objLinha.Peso.ToString();
                txtAltura.Text = objLinha.Altura.ToString();

                btnAlterar.Visible = true;
                btnExcluir.Visible = true;
                btnCalcular.Visible = false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
            EstadoInicial2();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            int linha;
            double peso, altura;

            linha = Convert.ToInt32(txtCod.Text);
            peso = Convert.ToDouble(txtPeso.Text);
            altura = Convert.ToDouble(txtAltura.Text);


            UsuarioVO2 objRecebe = PrepararObj(peso, altura);

            lstUsuarioVO[linha].Peso = objRecebe.Peso;
            lstUsuarioVO[linha].Altura = objRecebe.Altura;
            lstUsuarioVO[linha].Situacao= objRecebe.Situacao;

            EstadoInicial2();
            LimparCampos();
            CarregarGrid();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja mesmo excluir o usuário?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int linha;

                linha = Convert.ToInt32(txtCod.Text);

                lstUsuarioVO.RemoveAt(linha);
                CarregarGrid();
                LimparCampos();
                EstadoInicial2();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtLogin.Text.Trim() == "")
            {
                MessageBox.Show("Preencha o Login corretamente!");
            }
            else if (txtSenha.Text.Trim() == "")
            {
                MessageBox.Show("Preencha a Senha corretamente!");
            }
            else
            {
                if (txtSenha.Text == "5566")
                {
                    EstadoInicial2();
                }
                else
                {
                    MessageBox.Show("Senha Incorreta!");
                }
            }
        }
    }
}
