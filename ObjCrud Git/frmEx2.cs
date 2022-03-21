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
    public partial class frmEx2 : Form
    {
        public frmEx2()
        {
            InitializeComponent();
        }

        List<AlunosVO> lstAlunosVO = new List<AlunosVO>();

        private void frmEx2_Load(object sender, EventArgs e)
        {
            EstadoInicial();
        }
        private void EstadoInicial()
        {
            btnAlterar.Visible = false;
            btnCadastrar.Visible = false;
            btnCancelar.Visible = false;
            btnExcluir.Visible = false;
            txtN1.Visible = false;
            txtN2.Visible = false;
            txtN3.Visible = false;
            txtN4.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            grdAlunos.Visible = false;
        }
        private void EstadoInicial2()
        {
            btnAlterar.Visible = false;
            btnCadastrar.Visible = true;
            btnCancelar.Visible = true;
            btnExcluir.Visible = false;
            txtN1.Visible = true;
            txtN2.Visible = true;
            txtN3.Visible = true;
            txtN4.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            grdAlunos.Visible = true;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            double nota1, nota2, nota3, nota4, media;
            string situacao;

            nota1 = Convert.ToDouble(txtN1.Text);
            nota2 = Convert.ToDouble(txtN2.Text);
            nota3 = Convert.ToDouble(txtN3.Text);
            nota4 = Convert.ToDouble(txtN4.Text);
            CalculoMedia objMedia = new CalculoMedia();
            media = objMedia.CalcMedia(nota1, nota2, nota3, nota4);
            situacao = objMedia.Situacao(media);
            AlunosVO objRecebe = RetornaAluno(nota1, nota2, nota3, nota4, media, situacao);
            AdicionarGrid(objRecebe);
            CarregarGrid();
            LimparCampos();


        }
        private AlunosVO RetornaAluno(double nota1, double nota2, double nota3, double nota4, double media, string situacao)
        {
            AlunosVO objAlunos = new AlunosVO();

            objAlunos.Nota1 = nota1;
            objAlunos.Nota2 = nota2;
            objAlunos.Nota3 = nota3;
            objAlunos.Nota4 = nota4;
            objAlunos.Media = media;
            objAlunos.Situacao = situacao;

            return objAlunos;
        }
        private void AdicionarGrid(AlunosVO objAlunos)
        {
            lstAlunosVO.Add(objAlunos);
        }
        private void CarregarGrid()
        {
            grdAlunos.DataSource = null;
            grdAlunos.DataSource = lstAlunosVO;
        }
        private void LimparCampos()
        {
            txtN1.Clear();
            txtN2.Clear();
            txtN3.Clear();
            txtN4.Clear();
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtLogin.Text.Trim() == "")
            {
                MessageBox.Show("Preencha o Login corretamente");
            }
            else if (txtSenha.Text.Trim() == "")
            {
                MessageBox.Show("Preencha a Senha corretamente");
            }
            else
            {
                if(txtSenha.Text == "2020")
                {
                    btnAlterar.Visible = true;
                    btnCadastrar.Visible = true;
                    btnCancelar.Visible = true;
                    btnExcluir.Visible = true;
                    txtN1.Visible = true;
                    txtN2.Visible = true;
                    txtN3.Visible = true;
                    txtN4.Visible = true;
                    label1.Visible = true;
                    label2.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    grdAlunos.Visible = true;
                    btnLogin.Visible = false;
                    txtLogin.Visible = false;
                    txtSenha.Visible = false;
                    lblLogin.Visible = false;
                    lblSenha.Visible = false;
                }
                else
                {
                    MessageBox.Show("Senha Incorreta!");
                }

            }
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            if (txtLogin.Text.Trim() == "")
            {
                MessageBox.Show("Preencha o Login corretamente");
            }
            else if (txtSenha.Text.Trim() == "")
            {
                MessageBox.Show("Preencha a Senha corretamente");
            }
            else
            {
                if (txtSenha.Text == "2020")
                {
                    btnAlterar.Visible = false;
                    btnCadastrar.Visible = true;
                    btnCancelar.Visible = true;
                    btnExcluir.Visible = false;
                    txtN1.Visible = true;
                    txtN2.Visible = true;
                    txtN3.Visible = true;
                    txtN4.Visible = true;
                    label1.Visible = true;
                    label2.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    grdAlunos.Visible = true;
                    btnLogin.Visible = false;
                    txtLogin.Visible = false;
                    txtSenha.Visible = false;
                    lblLogin.Visible = false;
                    lblSenha.Visible = false;
                }
                else
                {
                    MessageBox.Show("Senha Incorreta!");
                }
            }
        }

        private void grdAlunos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdAlunos.RowCount > 0)
            {
                AlunosVO objLinha = (AlunosVO)grdAlunos.CurrentRow.DataBoundItem;

                txtCod.Text = e.RowIndex.ToString();
                txtN1.Text = objLinha.Nota1.ToString();
                txtN2.Text = objLinha.Nota2.ToString();
                txtN3.Text = objLinha.Nota3.ToString();
                txtN4.Text = objLinha.Nota4.ToString();

                btnAlterar.Visible = true;
                btnExcluir.Visible = true;
                btnCadastrar.Visible = false;



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
            double nota1, nota2, nota3, nota4;

            linha = Convert.ToInt32(txtCod.Text);
            nota1 = Convert.ToDouble(txtN1.Text);
            nota2 = Convert.ToDouble(txtN2.Text);
            nota3 = Convert.ToDouble(txtN3.Text);
            nota4 = Convert.ToDouble(txtN4.Text);

            lstAlunosVO[linha].Nota1 = nota1;
            lstAlunosVO[linha].Nota2 = nota2;
            lstAlunosVO[linha].Nota3 = nota3;
            lstAlunosVO[linha].Nota4 = nota4;

            LimparCampos();
            CarregarGrid();
            EstadoInicial2();

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja mesmo excluir o usuário?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int linha;

                linha = Convert.ToInt32(txtCod.Text);

                lstAlunosVO.RemoveAt(linha);
                CarregarGrid();
                LimparCampos();
                EstadoInicial2();
            }
        }

    }
}
