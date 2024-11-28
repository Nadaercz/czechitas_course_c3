namespace ToDoList.Frontend.Clients;

using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Frontend.View;

public interface IToDoItemsClient
{
    public Task<List<ToDoItemView>> ReadItemsAsync();
}
