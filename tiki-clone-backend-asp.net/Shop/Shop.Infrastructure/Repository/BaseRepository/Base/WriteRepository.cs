﻿using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Shop.Application;
using Shop.Application.UnitOfWork;
using Shop.Domain;
using Shop.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure
{
    public abstract class WriteRepository<T> : ReadRepository<T>, IWriteRepository<T> where T : class
    {
        protected DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;
        public WriteRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _dbContext = unitOfWork.DbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual async Task<T> CreateAsync(T entity, DbTransaction? dbContextTransaction = null)
        {
            if (dbContextTransaction != null)
            {
                _dbContext.Database.UseTransaction(dbContextTransaction);
                _ = await _dbSet.AddAsync(entity);
            }
            else
            {
                _ = await _dbSet.AddAsync(entity);
                _dbContext.SaveChanges();
            }
            return entity;
        }

        public virtual async Task<List<T>> CreateManyAsync(List<T> entities, DbTransaction? dbContextTransaction = null)
        {
            if (dbContextTransaction != null)
            {
                await _dbContext.Database.UseTransactionAsync(dbContextTransaction);
                await _dbContext.AddRangeAsync(entities);
            }
            else
            {
                await _dbSet.AddRangeAsync(entities);
                await _dbContext.SaveChangesAsync();
            }
            return entities;
        }

        public virtual async Task<T> Delete(T entity, DbTransaction? dbContextTransaction = null)
        {
            if (dbContextTransaction != null)
            {
                await _dbContext.Database.UseTransactionAsync(dbContextTransaction);
                _dbSet.Remove(entity);
            }
            else
            {
                _dbSet.Remove(entity);
                _dbContext.SaveChanges();
            }
            return entity;
        }

        public virtual async Task<int> DeleteManyAsync(List<T> entities, DbTransaction? dbContextTransaction = null)
        {
            if (dbContextTransaction != null)
            {
                await _dbContext.Database.UseTransactionAsync(dbContextTransaction);
                _dbSet.RemoveRange(entities);
            }
            else
            {
                _dbSet.RemoveRange(entities);
                _dbContext.SaveChanges();
            }
            return entities.Count;
        }

        public virtual async Task<T> UpdateAsync(T entity, DbTransaction? dbContextTransaction = null)
        {
            // ghép nối vào DBcontext
            _dbContext.Attach(entity);
            // thay đổi trạng thái của đối tượng trong context
            _dbContext.Entry(entity).State = EntityState.Modified;
            //_dbSet.Entry(entity).State = EntityState.Modified;
            if (dbContextTransaction != null)
            {
                await _dbContext.Database.UseTransactionAsync(dbContextTransaction);
                _dbSet.Update(entity);
            }
            else
            {
                _dbSet.Update(entity);
                _dbContext.SaveChanges();
            }
            return entity;
        }

        public virtual int UpdateMany(List<T> entities)
        {
            entities.ForEach(e =>
            {
                _dbContext.Attach(e);
                _dbContext.Entry(e).State = EntityState.Modified;
            });

            _dbSet.UpdateRange(entities);
            return entities.Count;
        }
    }
}
