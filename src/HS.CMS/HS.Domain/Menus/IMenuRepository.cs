
using HS.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace HS.IService.Menus
{
    public interface IMenuRepository: IServiceSupport
    {
        void Create(Menu model);
    }
}
