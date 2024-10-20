namespace ToDoList.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController : ControllerBase
{
    private static readonly List<ToDoItem> items = [];

    [HttpPost]
    public IActionResult Create(ToDoItemCreateRequestDto request)
    {
        //map to Domain object as soon as possible
        var item = request.ToDomain();

        //try to create an item
        try
        {
            item.ToDoItemId = items.Count == 0 ? 1 : items.Max(o => o.ToDoItemId) + 1;
            items.Add(item);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return NoContent(); //201 //tato metoda z nějakého důvodu vrací status code No Content 204, zjištujeme proč ;)
    }

    [HttpGet]
    public ActionResult<IEnumerable<ToDoItemGetResponseDto>> Read()
    {
        List<ToDoItem> itemsToGet;
        try
        {
            itemsToGet = items;
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }
        //respond to client
        return (itemsToGet is null)
            ? NotFound() //404
            : Ok(itemsToGet.Select(ToDoItemGetResponseDto.FromDomain)); //200
    }

    [HttpGet("{toDoItemId:int}")]
    public IActionResult ReadById(int toDoItemId)
    {
        ToDoItemCreateRequestDto todoItem;
        try
        {
            var item = items.Find(x => x.ToDoItemId == toDoItemId);
            todoItem = new(item.Name, item.Description, item.IsCompleted);

        }
        catch (ArgumentNullException ex)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

        return Ok();
    }

    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        try
        {
            var newItem = request.ToDomain();
            var todoItem = items.Find(i => i.ToDoItemId == toDoItemId);
            if (todoItem == null)
            {
                return NotFound();
            }
            int indexOfInstance = items.FindIndex(i => i.ToDoItemId == toDoItemId);
            items[indexOfInstance] = newItem;
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

        return NoContent();
    }

    [HttpDelete("{toDoItemId:int}")]
    public IActionResult DeleteById(int toDoItemId)
    {
        ToDoItem todoItem;
        try
        {
            todoItem = items.Find(x => x.ToDoItemId == toDoItemId);
        }
        catch (ArgumentNullException ex)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
        var isRemoted = items.Remove(todoItem);

        if (!isRemoted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
