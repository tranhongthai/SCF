
using Peyton.Core;

namespace System.Web.Mvc
{
    public class BaseController: Controller 
    {        
        public BaseController() { }

        protected void Log(string message)
        {
            Logger.LogInfo(message);
        }
    }
}
