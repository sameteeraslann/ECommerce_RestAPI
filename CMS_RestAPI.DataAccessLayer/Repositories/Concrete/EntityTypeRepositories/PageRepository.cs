using CMS_RestAPI.DataAccessLayer.Context;
using CMS_RestAPI.DataAccessLayer.Repositories.Concrete.Base;
using CMS_RestAPI.DataAccessLayer.Repositories.Interfaces.EntityTypeRepositories;
using CMS_RestAPI.EntityLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS_RestAPI.DataAccessLayer.Repositories.Concrete.EntityTypeRepositories
{
    public class PageRepository :KernelRepository<Page> ,IPageRepository
    {
        public PageRepository(ApplicationDbContext applicationDbContext): base(applicationDbContext){ }
    }
}
