using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TRILHAR.Business.Extensions
{
    public static class DataExtensions
    {
        /// <summary>
        /// Verifica se a pessoa faz aniversário em uma data específica
        /// </summary>
        /// <param name="dataNascimento">Data de nascimento</param>
        /// <param name="dataReferencia">Data para verificar (opcional, padrão é hoje)</param>
        /// <returns>true se for aniversário na data de referência, false caso contrário</returns>
        public static bool EhAniversarioNaData(DateTime dataNascimento, DateTime? dataReferencia = null)
        {
            try
            {
                // Se dataReferencia for null, usa a data atual
                DateTime referencia = dataReferencia ?? DateTime.Now;

                // Compara mês e dia
                return dataNascimento.Month == referencia.Month &&
                       dataNascimento.Day == referencia.Day;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao verificar aniversário: {ex.Message}");
                return false;
            }
        }

        // Sobrecarga para aceitar strings
        public static bool EhAniversarioNaData(string dataNascimento, string? dataReferencia = null)
        {
            try
            {
                DateTime nascimento = DateTime.Parse(dataNascimento);
                DateTime referencia = string.IsNullOrEmpty(dataReferencia)
                    ? DateTime.Now
                    : DateTime.Parse(dataReferencia);

                return EhAniversarioNaData(nascimento, referencia);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao verificar aniversário: {ex.Message}");
                return false;
            }
        }
    }
}
