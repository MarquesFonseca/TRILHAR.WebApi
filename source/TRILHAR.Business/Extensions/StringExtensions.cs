using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TRILHAR.Business.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string? value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrWhiteSpace(this string? value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static string RemoverAcentos(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            var normalized = value.Normalize(NormalizationForm.FormD);
            var chars = normalized.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark);
            return new string(chars.ToArray()).Normalize(NormalizationForm.FormC);
        }

        public static bool ContainsIgnoreCaseAndAccent(this string source, string value)
        {
            if (source == null || value == null) return false;

            string src = source.RemoverAcentos().ToLowerInvariant();
            string val = value.RemoverAcentos().ToLowerInvariant();
            return src.Contains(val);
        }

        public static bool EqualsIgnoreCaseAndAccent(this string source, string value)
        {
            if (source == null || value == null) return false;

            string src = source.RemoverAcentos().ToLowerInvariant();
            string val = value.RemoverAcentos().ToLowerInvariant();
            return src == val;
        }

        public static bool IsNumeric(this string value)
        {
            return double.TryParse(value, out _);
        }

        public static bool IsEmail(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;

            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase);
        }

        public static bool IsCpf(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;

            var cpf = Regex.Replace(value, @"[^\d]", "");

            if (cpf.Length != 11 || cpf.Distinct().Count() == 1)
                return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var tempCpf = cpf[..9];
            var soma = tempCpf.Select((t, i) => int.Parse(t.ToString()) * multiplicador1[i]).Sum();
            var resto = soma % 11;
            var digito1 = resto < 2 ? 0 : 11 - resto;

            tempCpf += digito1;
            soma = tempCpf.Select((t, i) => int.Parse(t.ToString()) * multiplicador2[i]).Sum();
            resto = soma % 11;
            var digito2 = resto < 2 ? 0 : 11 - resto;

            return cpf.EndsWith($"{digito1}{digito2}");
        }

        public static bool IsCnpj(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;

            var cnpj = Regex.Replace(value, @"[^\d]", "");

            if (cnpj.Length != 14 || cnpj.Distinct().Count() == 1)
                return false;

            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            var tempCnpj = cnpj[..12];
            var soma = tempCnpj.Select((t, i) => int.Parse(t.ToString()) * multiplicador1[i]).Sum();
            var resto = soma % 11;
            var digito1 = resto < 2 ? 0 : 11 - resto;

            tempCnpj += digito1;
            soma = tempCnpj.Select((t, i) => int.Parse(t.ToString()) * multiplicador2[i]).Sum();
            resto = soma % 11;
            var digito2 = resto < 2 ? 0 : 11 - resto;

            return cnpj.EndsWith($"{digito1}{digito2}");
        }

        public static string SomenteNumeros(this string value)
        {
            return Regex.Replace(value ?? string.Empty, @"[^\d]", "");
        }
    }
}
