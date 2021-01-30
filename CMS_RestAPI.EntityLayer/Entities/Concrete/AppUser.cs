using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS_RestAPI.EntityLayer.Entities.Concrete
{
    public class AppUser : BaseEntity<int>
    {
        [Required(ErrorMessage = "Boş Geçemezsin")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Boş Geçemezsin")]
        public string Password { get; set; }
        [NotMapped]
        public SecurityToken Token { get; set; }
    }
}
