using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Frontend.View;

namespace ToDoList.Frontend.Clients;

public interface IToDoItemsClient
{
    public Task<List<ToDoItemView>> ReadItemsAsync();
}
