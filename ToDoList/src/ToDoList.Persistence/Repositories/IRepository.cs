namespace ToDoList.Persistence.Repositories;

public interface IRepository<T> where T : class
{
    public void Create(T item);

    public List<T> Read();

    public T? ReadById(int itemId);

    public void UpdateById(T item);

    public void DeleteById(int itemId);
}