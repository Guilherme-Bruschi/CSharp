using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace objCrud
{
    public class CalculoIMC
    {
        public double CalcIMC (double peso, double altura)
        {
            double res;

            res = peso / (altura * altura);

            return res;
        }
        public string Situacao(double resultado)
        {
            string sit = "";

            if (resultado >= 0 && resultado <= 20)
            {
                sit = "Abaixo do Peso";
            }
            else if (resultado >= 21 && resultado <= 25)
            {
                sit = "Peso Ideal";
            }
            else if (resultado >= 26 && resultado <= 30)
            {
                sit = "Acima do Peso";
            }
            else
            {
                sit = "Obeso";
            }

            return sit;

        }
    }
}
