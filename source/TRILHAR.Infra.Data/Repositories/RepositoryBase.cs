using Microsoft.EntityFrameworkCore;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.IO.Paginacao;
using TRILHAR.Infra.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TRILHAR.Infra.Data.Repositories
{
    public abstract class RepositoryBase<TDbContext, TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase where TDbContext : DbContext
    {
        protected readonly TDbContext Context;
        protected readonly DbSet<TEntity> DbSet;


        public RepositoryBase(TDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public virtual async Task<TEntity> Get(
            Expression<Func<TEntity, bool>> expressionSearch,
            List<Expression<Func<TEntity, object>>> expressionIncludes = null)
        {
            var query = DbSet as IQueryable<TEntity>;
            query = query.AddIncludes(expressionIncludes);
            return await query.FirstOrDefaultAsync(expressionSearch);
        }

        public virtual async Task<IEnumerable<TEntity>> GetMany(
            Expression<Func<TEntity, bool>> expressionSearch,
            List<Expression<Func<TEntity, object>>> expressionIncludes = null)
        {
            var query = DbSet as IQueryable<TEntity>;
            query = query.AddIncludes(expressionIncludes);

            return await query.Where(expressionSearch).ToListAsync();
        }

        public virtual async Task<Paginacao<TEntity>> GetPaged(
            Expression<Func<TEntity, bool>> expressionSearch,
            List<Expression<Func<TEntity, object>>> expressionIncludes,
            int page,
            int pageSize
            )
        {
            var query = DbSet as IQueryable<TEntity>;
            query = query.AddIncludes(expressionIncludes);

            var skip = (page - 1) * pageSize;

            var result = new Paginacao<TEntity>
            {
                TotalRegistros = await query.CountAsync(expressionSearch),
                Resultado = await query
                .Where(expressionSearch)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync()
            };
            return result;
        }

        public virtual async Task<Paginacao<TEntity>> GetPagedByListConditions(
            List<Expression<Func<TEntity, bool>>> expressionSearch,
            List<Expression<Func<TEntity, object>>> expressionIncludes,
            int page,
            int pageSize
            )
        {
            var query = DbSet as IQueryable<TEntity>;
            query = query.AddIncludes(expressionIncludes);

            foreach (var item in expressionSearch)
            {
                query = query.Where(item);
            }

            var skip = (page - 1) * pageSize;

            var result = new Paginacao<TEntity>
            {
                TotalRegistros = await query.CountAsync(),
                Resultado = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync()
            };
            return result;
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
            return entity;
        }

        public virtual async Task<List<TEntity>> AddRange(List<TEntity> listEntity)
        {
            DbSet.AddRange(listEntity);
            await SaveChanges();
            return listEntity;
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
            return entity;
        }

        public virtual async Task<List<TEntity>> UpdateRange(List<TEntity> listEntity)
        {
            DbSet.UpdateRange(listEntity);
            await SaveChanges();
            return listEntity;
        }

        public virtual async Task Remove(object id)
        {

            var result = DbSet.Find(id);
            DbSet.Remove(result);
            await SaveChanges();
        }

        public virtual async Task RemoveEntity(TEntity entity)
        {
            DbSet.Remove(entity);
            await SaveChanges();
        }

        public async Task<bool> Exists(Expression<Func<TEntity, bool>> expression)
        {
            return await DbSet.AsNoTracking().AnyAsync(expression);
        }

        public void Dispose()
        {

            Context.Dispose();
            //GC.Collect();
            GC.SuppressFinalize(this);
        }

        public async Task RemoveRange(IEnumerable<TEntity> obj)
        {
            DbSet.RemoveRange(obj);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Context.SaveChangesAsync();
        }
    }
}