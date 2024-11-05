namespace ToDoList.Persistence.Repositories;

using ToDoList.Domain.Models;

public class ToDoItemsRepository : IRepository<ToDoItem>
{
    private readonly ToDoItemsContext context;

    public ToDoItemsRepository(ToDoItemsContext context)
    {
        this.context = context;
    }

    public void Create(ToDoItem item)
    {
        context.ToDoItems.Add(item);
        context.SaveChanges();
    }

    public void Find(int toDoItemId)
    {
        var itemToDelete = context.ToDoItems.Find(toDoItemId);
    }

    public void DeleteById(int itemId)
    {
        var itemToDelete = context.ToDoItems.Find(itemId) ?? throw new KeyNotFoundException();
        context.ToDoItems.Remove(itemToDelete);
        context.SaveChanges();
    }

    public List<ToDoItem> Read() => context.ToDoItems.ToList();

    public ToDoItem? ReadById(int itemId) => context.ToDoItems.Find(itemId);

    public void UpdateById(ToDoItem? item)
    {
        //retrieve the item
        var itemToUpdate = context.ToDoItems.Find(item.ToDoItemId) ?? throw new KeyNotFoundException();
        context.Entry(itemToUpdate).CurrentValues.SetValues(item);
        context.SaveChanges();
    }
}
