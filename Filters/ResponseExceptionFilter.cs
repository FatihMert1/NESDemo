using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using NilveraDemo.DTOs.Local.Responses;
using NilveraDemo.Exceptions;
using NilveraDemo.Models;

namespace NilveraDemo.Filters
{
        public class ResponseExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            if(context.Exception is ResponseException)
            {
                var responseException = context.Exception;
                context.Result = new Result(new ApiResponse<object>{Message=responseException.Message,Error=true,StatusCode=3});
            }
            return Task.CompletedTask;
        }
    }
}