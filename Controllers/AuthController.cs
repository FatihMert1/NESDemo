using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NilveraDemo.DTOs.Remote.Responses;
using NilveraDemo.Helpers;
using NilveraDemo.DTOs.Remote.Requests;
using RestSharp;
using NilveraDemo.DTOs.Local.Responses;
using NilveraDemo.DTOs.Local.Requests;

namespace NilveraDemo.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase{

        [HttpPost("login")]
        public async Task<ApiResponse<TokenResponse>> Login(LoginRequest loginRequest){

            var headers = new Dictionary<string,string>();
            headers.Add("content-type","application/x-www-form-urlencoded");

            var paramters = ClientHelper.AddParameter(loginRequest);

            var requestModel = new RemoteApiRequest{ Route="token", Parameters = paramters, Headers = headers, Method= Method.POST };
            
            return await ClientHelper.SendRequest<TokenResponse>(requestModel);
        }

    }
}