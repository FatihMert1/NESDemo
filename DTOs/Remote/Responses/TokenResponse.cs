namespace NilveraDemo.DTOs.Remote.Responses{
    public class TokenResponse {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public string Error { get; set; }
    }
}