Imports System.Web.Mvc
Imports $rootnamespace$.Microsoft.Web.Mvc
 
<Assembly: WebActivator.PreApplicationStartMethod(GetType($rootnamespace$.App_Start.MobileViewEngines), "Start")>
Namespace $rootnamespace$.App_Start
    Public NotInheritable Class MobileViewEngines
        Public Shared Sub Start()
            ViewEngines.Engines.Insert(0, New MobileCapableRazorViewEngine())
            ViewEngines.Engines.Insert(0, New MobileCapableWebFormViewEngine())
        End Sub
    End Class
End Namespace
