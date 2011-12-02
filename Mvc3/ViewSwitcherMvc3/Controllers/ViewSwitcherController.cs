using System.Web.Mvc;

namespace ViewSwitcherMvc3.Controllers
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