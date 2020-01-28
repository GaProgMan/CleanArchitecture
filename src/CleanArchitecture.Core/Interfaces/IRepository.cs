using System.Collections.Generic;
using CleanArchitecture.SharedKernel;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IRepository
    {
        T GetById<T>(int id) where T : BaseEntity;
        T GetById<T>(int id, string include) where T : BaseEntity;
        List<T> List<T>() where T : BaseEntity;
        List<T> List<T>(ISpecification<T> spec = null) where T : BaseEntity;
        T Add<T>(T entity) where T : BaseEntity;
        void Update<T>(T entity) where T : BaseEntity;
        void Delete<T>(T entity) where T : BaseEntity;
        int Count<T>() where T : BaseEntity;
    }
}