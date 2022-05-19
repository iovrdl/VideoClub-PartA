using System.Web.Mvc;
using VideoClub.Data.Services;
using VideoClub.Web.Models;

namespace VideoClub.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public ActionResult Index()
        {
            var model = _customerRepository.GetAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Rent(string id)
        {
            return RedirectToAction("Create", "Rentals", new { userId = id });
        }
        
        [HttpGet]
        public ActionResult Return(string id)
        {
            return RedirectToAction("Return", "Rentals", new { Id = id });
        }

        [HttpGet]
        public ActionResult UnreturnedRentals(string id)
        {
            return RedirectToAction("Index", "Rentals", new { userId = id });
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            var model = _customerRepository.Get(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }
    }
}