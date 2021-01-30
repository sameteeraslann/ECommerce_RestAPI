using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_RestAPI.UI.Models.DTOs
{
    public class AppUserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public SecurityToken Token { get; set; }
    }
}
