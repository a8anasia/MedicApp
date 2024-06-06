using MedicApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MedicApp.Controllers
{
    public class DoctorController : Controller
    {
        public List<Error> ErrorArray { get; set; } = new();

        private readonly IApplicationService _applicationService;

        public DoctorController(IApplicationService applicationService) : base()
        {
            _applicationService = applicationService;
        }

        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Index(int id)
        {
            var doctor = await _applicationService.DoctorService.GetDoctorByUserIdAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return View("Index", doctor);
        }

    }
}
