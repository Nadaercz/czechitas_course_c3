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
    public List<ToDoItem> Read()
    {
        //implement please :)
        return items;
    }

    [HttpGet("{toDoItemId:int}")]
    public IActionResult ReadById(int toDoItemId)
    {
        ToDoItemCreateRequestDto todoItem; //this should be ToDoItemGetRequestDto
        try
        {
            var item = items.Find(x => x.ToDoItemId == toDoItemId);
            //please first read the comment in catch (ArgumentNullException ex) block :)

            //have you read it? Great :)
            //i would test if item is null or not. If its null -> return NotFound()

            //this is unnecessary, you probably had to do this because you are missing implementation of ToDoItemGetRequestDto
            //it is at the end of Assignment 03.01.md file
            //then it is simple  todoItem = ToDoItemGetResponseDto.FromDomain(item);
            todoItem = new(item.Name, item.Description, item.IsCompleted);

        }
        catch (ArgumentNullException ex)
        {
            /*this functionality definitely fulfills the assignment, but this shloud be handled in "regular code block" - I mean in try {}
            to avoid handling exceptions. You want to handle exceptions when something bad happens, not when it is expected scenario*/
            return NotFound();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

        return Ok(); //we want to return Ok with ToDoItemGetRequestDto in body :)
    }

    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        try
        {
            var newItem = request.ToDomain();

            /*this block of code is unnecessary, this check can be done after you call FindIndex method. You dont need to call Find method.
            if no object is found in the list that would fulfill condition in FindIndex (i.e. i => i.ToDoItemId == toDoItemId), this
            method would return -1, so the check would be
            if (indexOfInstance == -1)
            {
                return NotFound();
            }
            */
            var todoItem = items.Find(i => i.ToDoItemId == toDoItemId);
            if (todoItem == null)
            {
                return NotFound();
            } //end of unnecessary code :)

            int indexOfInstance = items.FindIndex(i => i.ToDoItemId == toDoItemId);

            //something is missing :)
            //this will cause that the updated item will lost its id
            //newItem.ToDoItemId = toDoItemId and it is fixed :)
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
        ToDoItem todoItem; //we dont need this outside the try{} block :)
        try
        {
            todoItem = items.Find(x => x.ToDoItemId == toDoItemId);
        }
        catch (ArgumentNullException ex)
        {
            //again, i would move this situation into the try{} block (check if the todoItem is not null)
            return NotFound();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

        /* this section here is unnecessary, only move items.Remove(todoItem) into the try{} block
        it will be
        todoItem = items.Find(x => x.ToDoItemId == toDoItemId);
        check that it is not null, if it is true -> return NotFound();
        items.Remove(todoItem);
        */
        //you can then remove this code snippet :)
        var isRemoted = items.Remove(todoItem);

        if (!isRemoted)
        {
            return NotFound();
        }
        return NoContent();//end of unnecessary code
    }
}
