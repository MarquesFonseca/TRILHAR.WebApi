using TRILHAR.Business.Enums;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using TRILHAR.Business.Entities;

namespace TRILHAR.Business.Extensions
{
    public static class ObjectExtension
    {
        public static string ToStringNullable(this object value, int totalPadLeft = 0)
        {
            var ret = string.Empty;

            if (value == null)
            {
                return ret;
            }

            if (totalPadLeft == 0)
            {
                return value.ToString();
            }
            else
            {
                return value.ToString().PadLeft(totalPadLeft, '0');
            }
        }

        public static int ToInt(this object value)
        {
            int ret = 0;

            if (value == null)
            {
                return ret;
            }

            if (value.GetType().IsEnum)
            {
                return (int) value;
            }

            int.TryParse(value.ToString(), out ret);
            return ret;
        }

        public static long ToLong(this object value)
        {
            long ret = 0;

            if (value == null)
            {
                return ret;
            }

            long.TryParse(value.ToString(), out ret);
            return ret;
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
        
        public static bool IsInt(this object value)
        {
            if (value == null)
            {
                return false;
            }

            return int.TryParse(value.ToString(), out _);
        }        
    }
}