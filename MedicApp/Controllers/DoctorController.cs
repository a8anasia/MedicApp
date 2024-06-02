using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicApp.Controllers
{
    public class DoctorController : Controller
    {
        [Authorize(Roles = "Doctor")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
