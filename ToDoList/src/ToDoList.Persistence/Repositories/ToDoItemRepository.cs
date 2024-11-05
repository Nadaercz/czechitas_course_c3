namespace ToDoList.Persistence.Reppositories;

using ToDoList.Domain.Models;

public class ToDoItemRepository : IRepository<ToDoItem>
{
    public void Create(ToDoItem item) => throw new NotImplementedException();
}