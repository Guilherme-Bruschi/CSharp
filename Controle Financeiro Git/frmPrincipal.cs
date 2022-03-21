using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControleFinanceiro
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }



        private void label3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCategoria_Click(object sender, EventArgs e)
        {
            frmCategoria f = new frmCategoria();
            f.ShowDialog();
            painelslide.Height = btnCategoria.Height;
            painelslide.Top = btnCategoria.Top; 
        }

        private void btnMovimento_Click(object sender, EventArgs e)
        {
            frmMovimento f = new frmMovimento();
            f.ShowDialog();
            painelslide.Height = btnMovimento.Height;
            painelslide.Top = btnMovimento.Top;
        }

        private void btnConta_Click(object sender, EventArgs e)
        {
            frmConta f = new frmConta();
            f.ShowDialog();
            painelslide.Height = btnConta.Height;
            painelslide.Top = btnConta.Top;
        }

        private void btnEmpresa_Click(object sender, EventArgs e)
        {
            frmEmpresa f = new frmEmpresa();
            f.ShowDialog();
            painelslide.Height = btnEmpresa.Height;
            painelslide.Top = btnEmpresa.Top;
        }
    }
}
