using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Infra.Data.Extensions
{
    public static class PageResultExtensions
    {
        //private const int MAX_PAGE_SIZE = 1000;

        //public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        //{
        //    pageSize = GetMaxPageSize(pageSize);

        //    var result = GetPagedResult(query, page, pageSize);
        //    result.TotalItens = query.Count();
        //    result.TotalPaginas = GetTotalPaginas(result, pageSize);

        //    var skip = GetSkipValue(page, pageSize);
        //    result.Dados = query.Skip(skip).Take(pageSize).ToList();

        //    return result;
        //}

        //public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        //{
        //    pageSize = GetMaxPageSize(pageSize);

        //    var result = GetPagedResult(query, page, pageSize);
        //    result.TotalItens = await query.CountAsync();
        //    result.TotalPaginas = GetTotalPaginas(result, pageSize);

        //    var skip = GetSkipValue(page, pageSize);
        //    result.Dados = await query.Skip(skip).Take(pageSize).ToListAsync();

        //    return result;
        //}

        //private static int GetSkipValue(int page, int pageSize) => (page - 1) * pageSize;

        //private static int GetTotalPaginas<T>(PagedResult<T> pagedResult, int pageSize) where T : class
        //{
        //    var pageCount = (double)pagedResult.TotalItens / pageSize;
        //    return (int)Math.Ceiling(pageCount);
        //}

        //private static PagedResult<T> GetPagedResult<T>(IQueryable<T> query, int page, int pageSize) where T : class
        //{
        //    var result = new PagedResult<T>();
        //    result.PaginaAtual = page;
        //    result.TamanhoPagina = pageSize;
        //    return result;
        //}

        //private static int GetMaxPageSize(int pageSize) => pageSize > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : pageSize;

















        private const int MAX_PAGE_SIZE = 1000;

        public static PagedResult<T> GetPaged<T>(this IList<T> query, int totalRegistros, int page, int pageSize) where T : class
        {
            pageSize = GetMaxPageSize(pageSize);

            var result = GetPagedResult(query, page, pageSize);
            result.TotalItens = totalRegistros;
            result.TotalPaginas = GetTotalPaginas(result, pageSize);

            var skip = GetSkipValue(page, pageSize);
            result.Dados = query.ToList();

            return result;
        }

        private static int GetSkipValue(int page, int pageSize) => (page - 1) * pageSize;

        private static int GetTotalPaginas<T>(PagedResult<T> pagedResult, int pageSize) where T : class
        {
            var pageCount = (double)pagedResult.TotalItens / pageSize;
            return (int)Math.Ceiling(pageCount);
        }

        private static PagedResult<T> GetPagedResult<T>(IList<T> query, int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>();
            result.PaginaAtual = page;
            result.TamanhoPagina = pageSize;
            return result;
        }

        private static int GetMaxPageSize(int pageSize) => pageSize > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : pageSize;

    }
}