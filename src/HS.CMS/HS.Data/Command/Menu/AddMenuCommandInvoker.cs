using HS.Infrastructure.Command;
using HS.IService.Menus;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HS.Data.Command.Menu
{
    /// <summary>
    /// 添加菜单
    /// </summary>
    public class AddMenuCommandInvoker : ICommandInvoker<AddMenuCommand, CommandResult>
    {
        private HSDbContext _context { get; set; }
        private IMenuRepository _menuRepository;
        public AddMenuCommandInvoker(HSDbContext context)
        {
            _context = context;
        }
        public CommandResult Execute(AddMenuCommand command)
        {
            //FindAllArea();
            return new CommandResult();
        }

        //static List<string> FindAllArea()
        //{
        //    //获取区域下的Assembly
        //    var controllers = Assembly.GetExecutingAssembly().GetTypes().Where(o => o.CustomAttributes.Where(f => f.AttributeType == typeof(AreaAttribute)).Any()).ToArray();
        //    var list = new List<Type>();
        //    var typeList = new List<Type>();
        //    var areaDic = new List<String>();
        //    //var assembly = Assembly.GetExecutingAssembly().GetTypes();
        //    //var controllers = assembly.Where(o => o.BaseType.FullName.Contains(typeof(Controller).FullName));
        //    /* var controllers = typeof(Controller).GetAllSubclasses(false).ToArray();*/
        //    foreach (var item in controllers)
        //    {
        //        //获取去域名
        //        var areaName = item.GetCustomAttributesData()
        //            ?.FirstOrDefault(f => f.AttributeType == typeof(AreaAttribute))
        //            ?.ConstructorArguments
        //            ?.FirstOrDefault()
        //            .Value
        //            ?.ToString();
        //        if (string.IsNullOrEmpty(areaName)) continue;
        //        // var asm = item.Assembly.GetExportedTypes();
        //        if (!list.Contains(item))
        //        {
        //            list.Add(item);
        //            if (!areaDic.Contains(areaName))
        //            {
        //                areaDic.Add(areaName);
        //            }
        //        }
        //    }
        //    return areaDic.ToList();
        //}
    }
}

