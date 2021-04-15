using System;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IUnitWork : IDisposable
    {
        IGenericRepository<TEntity> Repository <TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}