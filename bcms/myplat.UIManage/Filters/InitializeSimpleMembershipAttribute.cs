using System;
using System.Threading;
using System.Web.Mvc;

namespace myplat.UIManage.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
    }
}
