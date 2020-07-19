using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class AuthenticationDto
    {
        public string UserId { get; set; }
        public int OrgId { get; set; }
        public string Token { get; set; }
        public int UserRole { get; set; }
    }
}
