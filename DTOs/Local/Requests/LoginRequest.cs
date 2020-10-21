using System.ComponentModel.DataAnnotations;

namespace NilveraDemo.DTOs.Local.Requests{

    public class LoginRequest {

        public string grant_type { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}