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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }


        private bool ValidarCampos()
        {
            bool ret = true;
            string campos = "";

            if(txtEmail.Text.Trim() == "")
            {
                ret = false;
                campos = "- Email\n";
            }
            if (txtSenha.Text.Trim() == "")
            {
                ret = false;
                campos += "- Senha";
            }
            if(!ret)
            {
                MessageBox.Show("Preencher o(s) campo(s):\n" + campos, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return ret;

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            btnCadastro.Visible = false;
        }

        private void pnlFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            frmCadastro f = new frmCadastro();
            f.ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                UsuarioDAO objUserDAO = new UsuarioDAO();

                int codUser = objUserDAO.ValidarLogin(txtEmail.Text, txtSenha.Text);

                if (codUser == -1)
                {
                    MessageBox.Show("Usuário não encontrado!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCadastro.Visible = true;
                }
                else
                {
                    Usuario.CodigoLogado = codUser;
                    this.DialogResult = DialogResult.OK;
                }
            }
        }
    }
}
