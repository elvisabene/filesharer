using System.Collections.Generic;

namespace FileSharer.Data.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T item);

        IEnumerable<T> GetAll();

        T GetById(int id);

        void Update(int id, T newItem);

        void Delete(int id);
    }
}
