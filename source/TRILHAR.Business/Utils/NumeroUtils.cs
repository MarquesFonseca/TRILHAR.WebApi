using System;
using System.Collections.Generic;
using System.Globalization;

namespace TRILHAR.Business.Utils
{
    public class NumeroUtils
    {
        public static string FormatarValor(string valor)
        {
            try
            {
                var valorFormatado = Convert.ToDouble(valor.Substring(0, 15)).ToString("N2");
                return valorFormatado;
            }
            catch
            {
                return "0,00";
            }
        }

        public static string FormatarValorCom17Casas(string valorCom17Casas)
        {
            try
            {
                string v = valorCom17Casas.Replace("+", "");
                string valor = Convert.ToDouble(v.Substring(0, 15)).ToString();
                string centavos = v.Substring(15, 2).ToString();
                double d = double.Parse($"{valor},{centavos}");
                string valorFormatado = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", d).Replace("R$", "");
                return valorFormatado;
            }
            catch
            {
                return "0,00";
            }
        }

        public static string FormatarValorCasas(string valorComCasas)
        {
            try
            {               
                int intposdec = valorComCasas.IndexOf("+");

                intposdec -= 2;

                string valor = Convert.ToDouble(valorComCasas.Substring(0, intposdec)).ToString();
                string centavos = "00";

                if (valorComCasas.Length > intposdec + 2) { 
                    centavos = Convert.ToDouble(valorComCasas.Substring(intposdec, 2)).ToString();
                }

                double d = double.Parse($"{valor},{centavos}");
                string valorFormatado = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", d).Replace("R$", "");

                return valorFormatado;
            }
            catch
            {
                return "0,00";
            }
        }

        public static string FormatarValorCom17CasasNegativos(string valorCom17Casas)
        {
            try
            {
                string v = valorCom17Casas.Replace("+", "");
                string valor = Convert.ToDouble(v.Substring(0, 15)).ToString();
                string centavos = v.Substring(15, 2).ToString();
                double d = double.Parse($"{valor},{centavos}");
                string valorFormatado = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", d).Replace("R$", "");
                if (valorCom17Casas.Contains("-"))
                {
                    valorFormatado = "-" + valorFormatado;
                }
                return valorFormatado;
            }
            catch
            {
                return "0,00";
            }
        }

        public static string ConvertToString17Caractereres(string valor)
        {
            var valorFormatado = valor.Replace(".", "").Replace(",","").PadLeft(17, '0');
            return valorFormatado;
        }

        public static string FormatarValor(string valor, int quantidadecaracteres)
        {
            var valorFormatado = valor.Replace(".", "").Replace(",", "").PadLeft(quantidadecaracteres, '0');
            return valorFormatado;
        }

        public static string DecimalToExtenso(Decimal valor)
        {
            string[] struni = new string[] { "", "Um", "Dois", "Tres", "Quatro", "Cinco", "Seis", "Sete", "Oito", "Nove", "Dez", "Onze", "Doze", "Treze", "Quatorze", "Quinze", "Dezessis", "Dezessete", "Dezoito", "Dezenove", "Vinte" };
            string[] strdez = new string[] { "", "", "Vinte", "Trinta", "Quarenta", "Cinquenta", "Sessenta", "Setenta", "Oitenta", "Noventa" };
            List<string[]> strcen = new List<string[]>() { new string[] { "", "" }, new string[] { "Cem", "Cento" }, new string[] { "Duzentos", "Duzentos" }, new string[] { "Trezentos", "Trezentos" }, new string[] { "Quatrocentos", "Quatrocentos" }, new string[] { "Quinhentos", "Quinhentos" }, new string[] { "Seiscentos", "Seiscentos" }, new string[] { "Setecentos", "Setecentos" }, new string[] { "Oitocentos", "Oitocentos" }, new string[] { "Novecentos", "Novecentos" } };
            List<string[]> moeda = new List<string[]>() { new string[] { " Trilhao", " Trilhoes" }, new string[] { " Bilhao", " Bilhoes" }, new string[] { " Milhao", " Milhoes" }, new string[] { " Mil", " Mil" }, new string[] { " Real", " Reais" }, new string[] { " Centavo", " Centavos" }, };
            List<string[]> result = new List<string[]>();
            string[] arrValor = Decimal.Round(valor, 2).ToString("0|0|0,0|0|0,0|0|0,0|0|0,0|0|0.0|0").Replace(",", ",0|").Replace(",", ".").Split('.');
            for (int i = arrValor.Length - 1; i >= 0; i--)
            {
                string[] z = arrValor[i].Split('|');
                int a = Convert.ToInt32(z[0]);
                int b = Convert.ToInt32(z[1]);
                int c = Convert.ToInt32(z[2]);
                int d = Convert.ToInt32(b.ToString() + c.ToString());
                int k = Convert.ToInt32(a.ToString() + b.ToString() + c.ToString());
                string r = (d >= 1 && d <= 20) ? string.Format("{0}", k == 0 ? "" : struni[d]) : string.Format("{0}{1}{2}", strdez[b], c > 0 ? " e " : "", k == 0 ? "" : struni[c]);
                r = k < 100 ? r : string.Format("{0}{1}{2}", strcen[a][d == 0 ? 0 : 1], d == 0 ? "" : " e ", r);
                result.Add(new string[] { i.ToString(), k.ToString(), r, " e ", moeda[i][k == 1 ? 0 : 1] });
            }
            if (Convert.ToInt32(result[1][1]) == 0)
            {
                string xmoeda = result[1][4];
                for (int i = 2; i <= result.Count - 1; i++)
                {
                    if (Convert.ToInt32(result[i][1]) > 0)
                    {
                        result[i][4] += " " + (i == 3 || i == 4 || i == 5 ? " de " : "") + xmoeda;
                        result[i][3] = " e ";
                        break;
                    }
                }
            }
            for (int i = result.Count - 1; i >= 0; i--) { if (Convert.ToInt32(result[i][1]) == 0) result.Remove(result[i]); }
            result[0][3] = "";
            for (int i = 2; i <= result.Count - 1; i++) { result[i][3] = ", "; }
            string extenso = "";
            for (int i = 0; i <= result.Count - 1; i++) { extenso = result[i][2] + result[i][4] + result[i][3] + extenso; }
            return extenso.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
        }

        public static string RemoverZerosEsquerda(string texto)
        {
            while (texto.StartsWith("0"))
            {
                texto = texto.Remove(0, 1);
            }

            return texto;
        }
    }
}