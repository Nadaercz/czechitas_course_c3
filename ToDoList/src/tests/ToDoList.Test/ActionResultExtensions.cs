using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Test;

public static class ActionResultExtensions
{
    public static T? GetValue<T>(this ActionResult<T> result)
    {
        if (result.Result is ObjectResult objectResult)
        {
            // Проверка на тип ProblemDetails и возврат null, если это ошибка
            if (objectResult.Value is ProblemDetails)
            {
                return default;
            }
            return (T?)objectResult.Value;
        }

        return result.Value;
    }
}
