using System.Collections.Generic;

namespace FileSharer.Business.Services.Interfaces
{
    public interface IService<T>
    {
        void Add(T item);

        IEnumerable<T> GetAll();

        T GetById(int id);

        void Update(int id, T newItem);

        void Delete(int id);
    }
}
