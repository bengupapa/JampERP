using System.Web.Mvc;

namespace JAMP.Controllers
{
    [Authorize]
    public class AppController : Controller
    {
        //
        // GET: /App/

        public ActionResult Jamp()
        {
            return View();
        }

    }
}
