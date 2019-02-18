using HS.Data.Entities;
using HS.IService.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace HS.Data.Service
{
    public class MenuRepository : IMenuRepository
    {

        public IContextFactory _contextFactory { get; set; }

        public MenuRepository(IContextFactory contextFactory)
        {
            this._contextFactory = contextFactory;
        }

        public void Create(Menu model)
        {
            using (var ctx = _contextFactory.Create())
            {
                ctx.Menus.Add(model);
                ctx.SaveChanges();
            }
        }
    }
}
