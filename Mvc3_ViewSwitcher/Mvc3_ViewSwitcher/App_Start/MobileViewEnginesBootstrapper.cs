using System.Web.Mvc;
using Microsoft.Web.Mvc;
 
[assembly: WebActivator.PreApplicationStartMethod(typeof(Mvc3_ViewSwitcher.App_Start.MobileViewEngines), "Start")]
namespace Mvc3_ViewSwitcher.App_Start {
    public static class MobileViewEngines{
        public static void Start() 
        {
            ViewEngines.Engines.Insert(0, new MobileCapableRazorViewEngine());
            // ViewEngines.Engines.Insert(0, new MobileCapableWebFormViewEngine());
        }
    }
}
