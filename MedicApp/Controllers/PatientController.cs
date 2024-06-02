using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicApp.Controllers
{
    public class PatientController : Controller
    {
        [Authorize(Roles = "Patient")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
