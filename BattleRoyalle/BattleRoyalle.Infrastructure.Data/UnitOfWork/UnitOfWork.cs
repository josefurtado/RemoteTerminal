using BattleRoyalle.Domain.Interfaces.Repositories;
using BattleRoyalle.Domain.Interfaces.UnitOfWork;
using BattleRoyalle.Infrastructure.Data.Contexts;
using BattleRoyalle.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;

namespace BattleRoyalle.Infrastructure.Data.UnitOfWork
{
    public class UnityOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private bool disposed = false;
        private readonly BattleRoyalleContext _context;
        private Dictionary<Type, object> _repositories;


        public UnityOfWork(BattleRoyalleContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IDbConnection DbConnection => _context.Database.GetDbConnection();

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories == null)
                _repositories = new Dictionary<Type, object>();

            var type = typeof(T);

            if (!_repositories.ContainsKey(type))
                _repositories[type] = new Repository<T>(_context);

            return (IRepository<T>)_repositories[type];
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(disposing: true);

            GC.SuppressFinalize(this);

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (_repositories != null)
                    {
                        _repositories.Clear();
                    }

                    _context.Dispose();
                }
            }

            disposed = true;
        }
    }
}
