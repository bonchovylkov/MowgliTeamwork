using System.Linq;

namespace RecipeRepository
{
    public interface IRepository<T>
    {
        T Add(T item);
        T Update(int id, T item);
        void Delete(int id);
        void Delete(T item);
        T Get(int id);
        IQueryable<T> All();
    }
}
