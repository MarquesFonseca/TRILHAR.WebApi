using System;
using System.ComponentModel;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces;

namespace TRILHAR.Business.Extensions
{
    public class ObjectExtensionGenerics<T> : IObjectExtensionGenerics<T> where T : EntityBase
    {
        public T TrataCamposNulls(T entity)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(entity))
            {
                if (prop.PropertyType.FullName.Contains("System.DateTime"))
                {
                    if (prop.GetValue(entity) == null || Convert.ToDateTime(prop.GetValue(entity)) == Convert.ToDateTime("01/01/0001"))
                    {
                        // Lógica para tratamento quando o campo é null ou igual a "01/01/0001"
                        if (prop.PropertyType.FullName.Contains("System.Nullable")) //se aceita campo nullo
                        {
                            prop.SetValue(entity, null);
                        }
                    }
                }
                if (prop.PropertyType.FullName.Contains("System.Int"))
                {
                    if (prop.GetValue(entity) == null)
                    {
                        // Lógica para tratamento quando o campo é null ou igual a "01/01/0001"
                        if (prop.PropertyType.FullName.Contains("System.Nullable")) //se aceita campo nullo
                        {
                            prop.SetValue(entity, null);
                        }
                    }
                }
                if (prop.PropertyType.FullName.Contains("System.String"))
                {
                    if (prop.GetValue(entity) == null)
                    {
                        // Lógica para tratamento quando o campo é null ou igual a "01/01/0001"
                        if (prop.PropertyType.FullName.Contains("System.Nullable")) //se aceita campo nullo
                        {
                            prop.SetValue(entity, null);
                        }
                    }
                }
            }
            return entity;
        }
    }
}
