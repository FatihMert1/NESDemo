using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NilveraDemo.Consts;
using NilveraDemo.DTOs.Local.Responses;
using NilveraDemo.DTOs.Remote.Requests;
using RestSharp;

namespace NilveraDemo.Helpers
{
    public static class ClientHelper{
        private static RestClient Client  = new RestClient(Const.RemoteBaseApiAddress);

        public static async Task<ApiResponse<T>> SendRequest<T>(RemoteApiRequest postModel,bool contentValue=false){
            var request = new RestRequest(postModel.Route,postModel.Method);
            request.AddHeaders(postModel.Headers);
            if(postModel.Parameters != null)
                foreach (var paramter in postModel.Parameters)
                    request.AddParameter(paramter.Key,paramter.Value);

            var response = await Client.ExecuteAsync<T>(request);    
            
            return  new ApiResponse<T>{Data=response.Data, Error=!response.IsSuccessful, Message= response.IsSuccessful ?  "Success" : response.ErrorMessage , 
                StatusCode = Convert.ToInt32(response.StatusCode) };
        }

        public static async Task<ApiResponse<string>> SendRequestForContent<T>(RemoteApiRequest postModel){
            var request = new RestRequest(postModel.Route,postModel.Method);
            request.AddHeaders(postModel.Headers);
            if(postModel.Parameters != null)
                foreach (var paramter in postModel.Parameters)
                    request.AddParameter(paramter.Key,paramter.Value);

            var response = await Client.ExecuteAsync<string>(request);
            
            return new ApiResponse<string>{Data=response.Content, Error=!response.IsSuccessful, Message= response.IsSuccessful ?  "Success" : response.ErrorMessage , 
                StatusCode = Convert.ToInt32(response.StatusCode) };
        }

        public static Dictionary<string,object> AddParameter(object container){
            var dict = new Dictionary<string,object>();
            var properties = container.GetType().GetProperties();
            foreach (var prop in properties)
            {   
                dict.Add(prop.Name, prop.GetValue(container));
            }
            return dict;
        }
    }
}