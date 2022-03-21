using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class MovimentoVO
    {
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
        public string Data { get; set; }
        public string Categoria { get; set; }
        public string Empresa { get; set; }
        public string Conta { get; set; }
        public string Obs { get; set; }
    }
}
