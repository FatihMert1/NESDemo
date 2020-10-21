using Microsoft.AspNetCore.Mvc;
using NilveraDemo.DTOs.Local.Responses;

namespace NilveraDemo.Models{
    public class Result: ObjectResult{
        public Result(ApiResponse<object> response) : base(response)
        {
            
        }
    }
}