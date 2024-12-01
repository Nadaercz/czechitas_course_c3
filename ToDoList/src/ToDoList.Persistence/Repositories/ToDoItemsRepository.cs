namespace ToDoList.Persistence.Repositories;

using System.Collections.Generic;
using ToDoList.Domain.Models;

public class ToDoItemsRepository(ToDoItemsContext context) : IRepositoryAsync<ToDoItem>
{
    private readonly ToDoItemsContext context = context;

    public async Task CreateAsync(ToDoItem item)
    {
        context.ToDoItems.Add(item);
        context.SaveChanges();
    }
    public async Task<IEnumerable<ToDoItem>> ReadAllAsync() => context.ToDoItems.ToList();
    public async Task<ToDoItem?> ReadByIdAsync(int id) => context.ToDoItems.Find(id);
    public async Task UpdateAsync(ToDoItem item)
    {
        var foundItem = context.ToDoItems.Find(item.ToDoItemId) ?? throw new ArgumentOutOfRangeException($"ToDo item with ID {item.ToDoItemId} not found.");
        context.Entry(foundItem).CurrentValues.SetValues(item);
        context.SaveChanges();
        return;
    }

    public async Task DeleteByIdAsync(int id)
    {
        var item = context.ToDoItems.Find(id) ?? throw new ArgumentOutOfRangeException($"ToDo item with ID {id} not found.");
        context.ToDoItems.Remove(item);
        context.SaveChanges();
    }
}
