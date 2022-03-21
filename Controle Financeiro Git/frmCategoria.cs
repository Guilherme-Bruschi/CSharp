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
    public partial class frmCategoria : Form
    {
        public frmCategoria()
        {
            InitializeComponent();
        }

        private void frmCategoria_Load(object sender, EventArgs e)
        {
            EstadoInicial();
            LimparCampos();
            CarregarGrid();
                
        }

        private void CarregarGrid()
        {
            CategoriaDAO objDAO = new CategoriaDAO();

            List<tb_categoria> lstcategorias = objDAO.ConsultarCategoria(Usuario.CodigoLogado);

            //Carregamento da grid
            grdCategorias.DataSource = lstcategorias;

            //Oculta as colunas indesejadas
            grdCategorias.Columns["id_categoria"].Visible = false;
            grdCategorias.Columns["id_usuario"].Visible = false;
            grdCategorias.Columns["tb_usuario"].Visible = false;
            grdCategorias.Columns["tb_movimento"].Visible = false;

            //Altera o texto da coluna
            grdCategorias.Columns["nome_categoria"].HeaderText = "Categorias";
        }
        private void EstadoInicial()
        {
            btnSalvar.Text = "Cadastrar";
            btnExcluir.Visible = false;
        }
        private void LimparCampos()
        {
            txtCodigo.Clear();
            txtNome.Clear();
        }


        private bool ValidarCampos()
        {
            bool ret = true;
            string campos = "";

            if (txtNome.Text.Trim() == "")
            {
                ret = false;
                campos = "- Nome\n";
            }

            if (!ret)
            {
                MessageBox.Show("Preencher o(s) campo(s):\n" + campos, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return ret;
        }


        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                //Instanciar a classe CategoriaDAO p/ usar as operações

                CategoriaDAO objDAO = new CategoriaDAO();

                //Instanciar a classe tb_categoria para preencher os SETS (campos)

                tb_categoria objCategoria = new tb_categoria();

                //Preenche as propriedades do objeto

                objCategoria.nome_categoria = txtNome.Text.Trim();
                objCategoria.id_usuario = Usuario.CodigoLogado;

                try
                {
                    if (txtCodigo.Text == "")
                    {
                        //Chama a operação que cadastrará a categoria
                        objDAO.CadastrarCategoria(objCategoria);
                    }
                    else
                    {
                        objCategoria.id_categoria = Convert.ToInt32(txtCodigo.Text);
                        objDAO.AlterarCategoria(objCategoria);
                    }

                    EstadoInicial();
                    LimparCampos();
                    CarregarGrid();
                    MessageBox.Show("Ação realizada com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Ocorreu um erro, Tente novamente mais tarde.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }




            }
            EstadoInicial();
            LimparCampos();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void grdCategorias_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //Verifica se tem linhas
            if (grdCategorias.RowCount > 0)
            {
                //Recupera a linha clicada
                tb_categoria objLinhaClicada = (tb_categoria)grdCategorias.CurrentRow.DataBoundItem;

                //Preencher os campos
                txtCodigo.Text = Convert.ToString(objLinhaClicada.id_categoria);
                txtNome.Text = objLinhaClicada.nome_categoria;

                btnExcluir.Visible = true;
                btnSalvar.Text = "Alterar";
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            EstadoInicial();
            LimparCampos();
        }

        private void btnExcluir_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir a categoria?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int idCategoria = Convert.ToInt32(txtCodigo.Text);
                CategoriaDAO objDAO = new CategoriaDAO();

                try
                {
                    objDAO.ExcluirCategoria(idCategoria);
                    LimparCampos();
                    EstadoInicial();
                    CarregarGrid();
                    MessageBox.Show("Categoria excluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("A Categoria não poderá ser excluída pois está em uso!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
