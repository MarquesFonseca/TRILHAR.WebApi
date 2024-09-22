using System;
using System.Collections.Generic;
using System.Text;

namespace TRILHAR.Business.IO.Paginacao
{
    public class Paginacao<T>
    {
        public Paginacao(int totalRegistros, IEnumerable<T> resultado)
        {
            TotalRegistros = totalRegistros;
            Resultado = resultado;
        }

        public Paginacao()
        {
        }

        public int TotalRegistros { get; set; }
        public IEnumerable<T> Resultado { get; set; }
    }
}
