using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;


namespace KjosAssignment5.Controllers
{
    public class HomePageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
