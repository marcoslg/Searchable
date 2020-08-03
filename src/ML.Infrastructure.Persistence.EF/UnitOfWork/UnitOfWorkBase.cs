﻿using Microsoft.EntityFrameworkCore;
using ML.Data.Contracts.Respositories;
using ML.Data.Contracts.UnitOfWork;
using ML.Infrastructure.Persistence.EF.Repositories;
using System;
using System.Collections;

namespace Common.Infrastructure.EF.UnitOfWork
{
    public class UnitOfWorkBase : IUnitOfWork
    {
        private readonly DbContext _context;
        private bool _disposed;
        private Hashtable _repositories;
               
        public UnitOfWorkBase(DbContext context)
        {
            this._context = context;
        }

        public int Save()
        {
            return this._context.SaveChanges();
        }

        public virtual IWriteRepository<T> GetRepository<T>() where T : class
        {
            return (IWriteRepository<T>)this.GetRepository<T>(typeof(WriteRepository<>));
        }

        public virtual IQueryRepository<T> GetQueryRepository<T>() where T : class
        {
            return (IWriteRepository<T>)this.GetRepository<T>(typeof(QueryRepository<>));
        }

        protected object GetRepository<T>(Type repositoryType) where T : class
        {
            if (this._repositories == null)
            {
                this._repositories = new Hashtable();
            }
            string name = typeof(T).Name;
            if (!this._repositories.ContainsKey((object)name))
            {
                object newInstance = this.GetNewInstance<T>(repositoryType);
                this._repositories.Add((object)name, newInstance);
            }
            return this._repositories[(object)name];
        }

        private object GetNewInstance<T>(Type repositoryType) where T : class
        {
            if (repositoryType.IsGenericTypeDefinition)
            {
                return Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), new object[1]
                {
                (object) this._context
                });
            }
            return Activator.CreateInstance(repositoryType, new object[1]
            {
                (object) this._context
            });
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed && disposing)
                this._context.Dispose();
            this._disposed = true;
        }
    }
}
