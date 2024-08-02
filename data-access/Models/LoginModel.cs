using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Models
{
    public class LoginModel
    {
        public string email { get; set; } = "";
        public string username { get; set; } = "";
        public string password { get; set; } = "";
    }

    public class RegisterModel
    {
        public string email { get; set; } = "";
        public string password { get; set; } = "";
        public string username { get; set; } = "";

    }

    public class TokenModel
    {
        public string userName { get; set; } = "";
        public string access_token { get; set; } = "";
        public string token_type { get; set; } = "";
        public string refreshToken { get; set; } = "";
        public string expires_in { get; set; } = "";
        public string issued { get; set; } = "";
        public string expires { get; set; } = "";
        public string role { get; set; } = "";

    }
}
