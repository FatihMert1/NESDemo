using System.Collections.Generic;
using RestSharp;

namespace NilveraDemo.DTOs.Remote.Requests{
    public class RemoteApiRequest{
        public string Route { get; set; }
        public Dictionary<string,object> Parameters { get; set; }
        public Dictionary<string,string> Headers { get; set; }
        public Method Method { get; set; }
    }
}