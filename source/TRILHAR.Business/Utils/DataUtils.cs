namespace TRILHAR.Business.Utils
{
    public class DataUtils
    {
        public static string FormatarDataComBarra(string dataComPonto)
        {
            return dataComPonto.Replace(".", "/");
        }
    }
}