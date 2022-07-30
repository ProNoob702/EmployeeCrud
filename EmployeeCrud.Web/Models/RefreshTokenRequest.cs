using System.Text.Json.Serialization;

namespace EmployeeCrud.Web.Models
{
    public class RefreshTokenRequest
    {
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
