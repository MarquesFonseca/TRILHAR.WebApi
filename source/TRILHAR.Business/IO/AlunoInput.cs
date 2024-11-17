using System;
using System.Collections.Generic;
using System.Text;
using TRILHAR.Business.Entities;

namespace TRILHAR.Business.IO.Aluno
{
    public class AlunoInputwwwww
    {
        public string? Condicao { get; set; }
        public object? Parametros { get; set; }
        public bool IsPaginacao { get; set; } = false;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
