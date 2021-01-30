using CMS_RestAPI.DataAccessLayer.Repositories.Interfaces.Base;
using CMS_RestAPI.EntityLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS_RestAPI.DataAccessLayer.Repositories.Interfaces.EntityTypeRepositories
{
    public interface IPageRepository :IKernelRepository<Page>
    {
    }
}
