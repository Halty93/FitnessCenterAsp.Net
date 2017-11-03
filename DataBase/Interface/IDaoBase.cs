using System.Collections.Generic;

namespace DataBase.Interface
{
    public interface IDaoBase<T>
    {
        IList<T> GetAll();

        object Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        T GetById(int id);
    }
}