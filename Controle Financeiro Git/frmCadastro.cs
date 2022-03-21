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
    public partial class frmCadastro : Form
    {
        public frmCadastro()
        {
            InitializeComponent();
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            if(ValidarCampos())
            {
                CadastroDAO objDAO = new CadastroDAO();

                tb_usuario objUser = new tb_usuario();

                objUser.nome_usuario = txtNome.Text;
                objUser.email_usuario = txtEmail.Text;
                objUser.senha_usuario = txtSenha.Text;


                try
                {
                    objDAO.CadastrarUsuario(objUser);
                    MessageBox.Show("Usuário cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
                }
                catch 
                {
                    MessageBox.Show("Houve um erro ao cadastrar o usuário!\nTente novamente mais tarde.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);                
                }
            }
        }
        private void LimparCampos()
        {
            txtEmail.Clear();
            txtNome.Clear();
            txtRepetirSenha.Clear();
            txtSenha.Clear();
        }

        private bool ValidarCampos()
        {
            bool ret = true;
            string campos = "";

            if(txtNome.Text.Trim() == "")
            {
                ret = false;
                campos = "- Nome\n";
            }
            if(txtNome.Text.Trim() == "")
            {
                ret = false;
                campos += "- Senha\n";
            }
            else if(txtRepetirSenha.Text.Trim() != txtSenha.Text.Trim())
            {
                ret = false;
                campos += "- O campo senha e repetir senha não conferem";
            }
            if(txtEmail.Text.Trim() == "")
            {
                ret = false;
                campos += "- E-mail";
            }


            if (!ret)
            {
                MessageBox.Show("Preencher o(s) campo(s):\n" + campos, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return ret;
        }

        private void pnlFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
