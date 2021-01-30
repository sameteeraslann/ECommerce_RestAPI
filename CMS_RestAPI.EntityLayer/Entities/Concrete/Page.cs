using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS_RestAPI.EntityLayer.Entities.Concrete
{
    public class Page: BaseEntity<int>
    {
        [Required(ErrorMessage = "Must type a title")]
        [MinLength(2, ErrorMessage = "Minimum lenght is 2")]
        public string Title { get; set; }

        public string Slug { get; set; }

        [Required(ErrorMessage = "Must type a title")]
        [MinLength(2, ErrorMessage = "Minimum lenght is 2")]
        public string Content { get; set; }

        public int? Sorting { get; set; }
    }
}
