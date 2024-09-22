using System;

namespace TRILHAR.Business.Extensions
{
    public static class ByteArrayExtension
    {
        public static string ToImageBase64(this byte[] item)
        {
            return (item != null && item.Length > 0) ? $"data:image/png;base64,{Convert.ToBase64String(item)}" : "";
        }
    }
}