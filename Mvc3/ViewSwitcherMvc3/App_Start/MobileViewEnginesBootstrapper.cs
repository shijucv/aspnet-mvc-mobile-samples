using System.Web.Mvc;
using ViewSwitcherMvc3.App_Start;

[assembly: WebActivator.PreApplicationStartMethod(typeof(MobileViewEngines), "Start")]

namespace ViewSwitcherMvc3.App_Start {

    public static class MobileViewEngines{

        public static void Start() 
        {
            ViewEngines.Engines.Insert(0, new MobileCapableRazorViewEngine());
        }
    }

}