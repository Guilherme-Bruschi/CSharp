using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace objCrud
{
    public class CalculoMedia
    {
        public double CalcMedia(double n1, double n2, double n3, double n4)
        {
            double res;

            res = (n1 + n2 + n3 + n4) / 4;

            return res;
        }
        public string Situacao(double media)
        {
            string sit = "";

            if (media >= 0 && media <= 39)
            {
                sit = "Aluno Reprovado";
            }
            else if (media >= 40 && media <= 59)
            {
                sit = "Aluno em Exame";
            }
            else
            {
                sit = "Aluno Aprovado";
            }

            return sit;
        }
    }
}
