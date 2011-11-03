using System.Web.Mvc;
using System.Web.WebPages;

namespace Mvc_ViewSwitcher.Controllers
{
    public class ViewSwitcherController : Controller
    {
        public RedirectResult SwitchView(bool mobile, string returnUrl)
        {
            Response.Cookies["ViewSwitcher"]["Mobile"] = (mobile ? "true" : "false");

            return Redirect(returnUrl);
        }
    }
}
