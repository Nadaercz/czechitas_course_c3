namespace ToDoList.Persistence.Repositories;

using System.Collections.Generic;
using ToDoList.Domain.Models;

public class ToDoItemsRepository(ToDoItemsContext context) : IRepositoryAsync<ToDoItem>
{
    private readonly ToDoItemsContext context = context;

    public async Task CreateAsync(ToDoItem item)
    {
        context.ToDoItems.Add(item);
        await context.SaveChangesAsync();
    }
    public async Task<IEnumerable<ToDoItem>> ReadAllAsync() => context.ToDoItems.ToList();
    public async Task<ToDoItem?> ReadByIdAsync(int id) => await context.ToDoItems.FindAsync(id);
    public async Task UpdateAsync(ToDoItem item)
    {
        var foundItem = context.ToDoItems.Find(item.ToDoItemId) ?? throw new ArgumentOutOfRangeException($"ToDo item with ID {item.ToDoItemId} not found."); //can be also FindAsync
        context.Entry(foundItem).CurrentValues.SetValues(item);
        await context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var item = context.ToDoItems.Find(id) ?? throw new ArgumentOutOfRangeException($"ToDo item with ID {id} not found."); //can be also FindAsync
        context.ToDoItems.Remove(item);
        await context.SaveChangesAsync();
    }
}
