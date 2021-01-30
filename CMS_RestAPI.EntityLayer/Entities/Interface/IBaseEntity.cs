using CMS_RestAPI.EntityLayer.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS_RestAPI.EntityLayer.Entities.Interface
{
    public interface IBaseEntity
    {
        DateTime CreateDate { get; set; }
        DateTime? UpdateDate { get; set; }
        DateTime? DeleteDate { get; set; }
        
        Status Status { get; set; }
    }
}
