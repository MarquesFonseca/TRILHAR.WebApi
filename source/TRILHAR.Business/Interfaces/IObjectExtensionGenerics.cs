using System;
using TRILHAR.Business.Entities;

namespace TRILHAR.Business.Interfaces
{
    public interface IObjectExtensionGenerics<T> where T : EntityBase
    {
        T TrataCamposNulls(T entity);
    }
}
