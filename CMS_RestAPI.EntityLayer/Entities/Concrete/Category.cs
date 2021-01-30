using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS_RestAPI.EntityLayer.Entities.Concrete
{
    public class Category: BaseEntity<int>
    {
        [Required(ErrorMessage ="Boş Geçemezsin")]
        [MinLength(2, ErrorMessage = "En az 2 karakter giriniz ")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Yanlızca izin verilen harfler kullanınız.")]
        public string Name { get; set; }
        [Required]
        public string Slug { get; set; }

        public List<Product> Products { get; set; }
    }
}
