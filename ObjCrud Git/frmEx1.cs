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
    public partial class frmEx1 : Form
    {
        public frmEx1()
        {
            InitializeComponent();
        }

        List<UsuarioVO> lstUsuarioVO = new List<UsuarioVO>();

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            string nome, endereco;
            UsuarioVO objUsuario;

            nome = txtNome.Text.Trim();
            endereco = txtEndereco.Text.Trim();
            objUsuario = RetornaUsuario(nome, endereco);

            AdicionarLista(objUsuario);
            CarregarGrid();
            LimparCampos();
        }
        private UsuarioVO RetornaUsuario (string nome, string endereco)
        {
            UsuarioVO objUsuario = new UsuarioVO();

            objUsuario.Nome = nome;
            objUsuario.Endereco = endereco;

            return objUsuario;
        }
        private void AdicionarLista (UsuarioVO objUsuario)
        {
            lstUsuarioVO.Add(objUsuario);
        }
        private void CarregarGrid()
        {
            grdPessoas.DataSource = null;
            grdPessoas.DataSource = lstUsuarioVO;
        }
        private void LimparCampos()
        {
            txtNome.Clear();
            txtEndereco.Clear();
        }
        private void EstadoInicial()
        {
            btnAlterar.Visible = false;
            btnCadastrar.Visible = true;
            btnExcluir.Visible = false;

        }

        private void grdPessoas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Verifica se há linhas na grid
            if (grdPessoas.RowCount > 0)
            {
                //Recupera o OBJ linha clicada
                UsuarioVO objVoLinha = (UsuarioVO)grdPessoas.CurrentRow.DataBoundItem;
                //Armazena a posição da linha
                txtCod.Text = e.RowIndex.ToString();
                txtNome.Text = objVoLinha.Nome;
                txtEndereco.Text = objVoLinha.Endereco;

                btnAlterar.Visible = true;
                btnCadastrar.Visible = false;
                btnExcluir.Visible = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            EstadoInicial();
            LimparCampos();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            int linha;
            string nome, endereco;

            linha = Convert.ToInt32(txtCod.Text);
            nome = txtNome.Text;
            endereco = txtEndereco.Text;

            //Altera a posição da lista

            lstUsuarioVO[linha].Nome = nome;
            lstUsuarioVO[linha].Endereco = endereco;

            EstadoInicial();
            LimparCampos();
            CarregarGrid();

        }

        private void frmEx1_Load(object sender, EventArgs e)
        {
            EstadoInicial();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Deseja mesmo excluir o usuário?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int linha;

                linha = Convert.ToInt32(txtCod.Text);

                lstUsuarioVO.RemoveAt(linha);
                CarregarGrid();
                LimparCampos();
                EstadoInicial();
            }
        }
    }
}
