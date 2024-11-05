namespace ToDoList.Persistence.Reppositories;

using ToDoList.Domain.Models;

public interface IRepository<T> where T: class
{
    public void Create(T item);
    // TODO: doplnit ostatni methody
}