using NilveraDemo.DTOs.Local.Responses;

namespace NilveraDemo.Helpers{
    public class ResponseHelper{

       public static ApiResponse<T> CreateApiResponse<T>(T data, string message, int statusCode, bool error){
            return new ApiResponse<T>{
                Data=data, Message = message, StatusCode = statusCode, Error = error
            };
    }
}
}