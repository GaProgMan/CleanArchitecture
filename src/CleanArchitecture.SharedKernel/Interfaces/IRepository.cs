using System.Collections.Generic;

namespace CleanArchitecture.SharedKernel.Interfaces
{
    public interface IRepository
    {
        T GetById<T>(int id, string include) where T : BaseEntity;
        List<T> List<T>() where T : BaseEntity;
        T Add<T>(T entity) where T : BaseEntity;
        void Update<T>(T entity) where T : BaseEntity;
        void Delete<T>(T entity) where T : BaseEntity;
        int Count<T>() where T : BaseEntity;
    }
}