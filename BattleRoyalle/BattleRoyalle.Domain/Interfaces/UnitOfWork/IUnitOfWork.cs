using BattleRoyalle.Domain.Interfaces.Repositories;
using System;
using System.Data;

namespace BattleRoyalle.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection DbConnection { get; }
        void SaveChanges();
        IRepository<T> GetRepository<T>() where T : class;
    }
}
