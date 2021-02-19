using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using FeePay.Core.Application.DTOs;
using FeePay.Web.Services.Interfaces;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using FeePay.Web.Filters;

namespace FeePay.Web.Services
{
    public class MvcControllerDiscovery : IMvcControllerDiscovery
    {
        //private List<MvcControllerInfo> _mvcControllers;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public MvcControllerDiscovery(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        public IEnumerable<MvcControllerInfo> GetSchoolControllers(string selectedlist = "")
        {
            // TODO: make it dynamic
            var hideAction = new List<string>() { };
            var hideController = new List<string>() { "Authentication" };
            var DefaultController = new List<string>() { "Uitility", "Home" };
            var DefaultSelectedAction = new List<string>() { };
            var list = GetControllers()
                .Where(w =>
                w.AreaName != null &&
                w.AreaName.Equals("school", StringComparison.InvariantCultureIgnoreCase) && !hideController.Contains(w.Name))
                .ToList();
            // Filter Default 
            list.ForEach(f =>
            {
                f.Actions = f.Actions.Where(wa => !hideAction.Contains(wa.Name)).ToList();
                f.Actions.ToList().ForEach(fa =>
                {
                    fa.IsDisabled = DefaultSelectedAction.Contains(fa.Name);
                    fa.IsSelected = DefaultController.Contains(fa.Name);
                });
                f.IsDisabled = DefaultController.Contains(f.Name);
                f.IsSelected = DefaultController.Contains(f.Name);
            });
            // Filter Default Selected
            if (!string.IsNullOrEmpty(selectedlist))
            {
                var accessList = JsonConvert.DeserializeObject<IEnumerable<MvcControllerInfo>>(selectedlist);
                list.ForEach(f =>
                {
                    f.Actions.ToList().ForEach(fa =>
                    {
                        fa.IsSelected = accessList.SelectMany(c => c.Actions).Any(a => a.Id == fa.Id);
                    });
                    if (accessList.Any(a => a.Id == f.Id)) f.IsSelected = true;
                });
            }
            return list;
        }
        public IEnumerable<MvcControllerInfo> GetSuperAdminControllers(string selectedlist = "")
        {
            // TODO: make all dynamic with a new page
            var hideAction = new List<string>() { "TostMessage", "Message", "AlertMessage", "RedirectToLocal", "Logout" };
            var hideController = new List<string>() { "Authentication", "AreaBase" };
            var list = GetControllers()
                .Where(w => w.AreaName != null &&
                w.AreaName.Equals("superadmin", StringComparison.InvariantCultureIgnoreCase) &&
                !hideController.Contains(w.Name)
                ).ToList();
            list.ForEach(f =>
            {
                f.Actions = f.Actions.Where(wa => !hideAction.Contains(wa.Name)).ToList();
            });
            return list;
        }
        public IEnumerable<MvcControllerInfo> GetControllers()
        {
            //if (_mvcControllers != null)
            //    return _mvcControllers;

            List<MvcControllerInfo> _mvcControllers = new List<MvcControllerInfo>();

            var items = _actionDescriptorCollectionProvider
                .ActionDescriptors.Items
                .Where(descriptor => descriptor.GetType() == typeof(ControllerActionDescriptor))
                .Select(descriptor => (ControllerActionDescriptor)descriptor)
                .GroupBy(descriptor => descriptor.ControllerTypeInfo.FullName)
                .ToList();

            foreach (var actionDescriptors in items)
            {
                if (!actionDescriptors.Any())
                    continue;

                var actionDescriptor = actionDescriptors.First();
                var controllerTypeInfo = actionDescriptor.ControllerTypeInfo;
                var currentController = new MvcControllerInfo
                {
                    AreaName = controllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue,
                    DisplayName = controllerTypeInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                    Name = actionDescriptor.ControllerName,
                };

                var actions = new List<MvcActionInfo>();
                foreach (var descriptor in actionDescriptors.GroupBy(a => a.ActionName).Select(g => g.First()))
                {
                    var methodInfo = descriptor.MethodInfo;
                    if (IsProtectedAction(controllerTypeInfo, methodInfo))
                        actions.Add(new MvcActionInfo
                        {
                            ControllerId = currentController.Id,
                            Name = descriptor.ActionName,
                            DisplayName = methodInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                        });
                }

                currentController.Actions = actions;
                _mvcControllers.Add(currentController);
            }

            return _mvcControllers;
        }


        private static bool IsProtectedAction(MemberInfo controllerTypeInfo, MemberInfo actionMethodInfo)
        {
            if (actionMethodInfo.GetCustomAttribute<AllowAnonymousAttribute>(true) != null)
                return false;

            //if (controllerTypeInfo.GetCustomAttribute<SchoolAdminAuthorizeAttribute>(true) != null)
            //    return true;

            //if (actionMethodInfo.GetCustomAttribute<SchoolAdminAuthorizeAttribute>(true) != null)
            //    return true;


            //if (controllerTypeInfo.GetCustomAttribute<SuperAdminAuthorizeAttribute>(true) != null)
            //    return true;

            //if (actionMethodInfo.GetCustomAttribute<SuperAdminAuthorizeAttribute>(true) != null)
            //    return true;

            if (controllerTypeInfo.GetCustomAttribute<MvcDiscoveryAttribute>(true) != null)
                return true;

            if (actionMethodInfo.GetCustomAttribute<MvcDiscoveryAttribute>(true) != null)
                return true;

            return false;
        }
    }
}
