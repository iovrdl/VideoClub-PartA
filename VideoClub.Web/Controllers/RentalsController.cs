using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VideoClub.Data.Entities;
using VideoClub.Data.Services;
using VideoClub.Web.Models;

namespace VideoClub.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RentalsController : Controller
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IFilmRepository _filmRepository;
        private readonly ICopyRepository _copyRepository;
        private readonly ICustomerRepository _customerRepository;

        public RentalsController(IRentalRepository rentalRepository, IFilmRepository filmRepository,
            ICopyRepository copyRepository, ICustomerRepository customerRepository)
        {
            _rentalRepository = rentalRepository;
            _filmRepository = filmRepository;
            _copyRepository = copyRepository;
            _customerRepository = customerRepository;
        }

        public ActionResult Index(string userId)
        {
            var model = _rentalRepository.GetAll()
                .Where(r => r.Returned == false);

            if (!string.IsNullOrEmpty(userId))
            {
                model = model.Where(r => r.UserId.Equals(userId));
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var now = DateTime.Now;
            var model = new CreateRentalBindModel
            {
                RentalDate = now,
                ReturnDate = now.AddDays(7),
                Films = new List<Film>(_filmRepository.GetAll()),
                Customers = new List<User>(_customerRepository.GetAll())
            };

            try
            {
                var filmId = int.Parse(Request.Params["filmId"]);
                model.FilmId = filmId;
            }
            catch (Exception e)
            {
                // ignored
            }

            var userId = Request.Params["userId"];
            if (!string.IsNullOrEmpty(userId))
            {
                model.UserId = userId;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateRentalBindModel model)
        {
            if (ModelState.IsValid)
            {
                var film = _filmRepository.Get(model.FilmId);
                var copy = film.Copies.First(c => c.Available);

                var rental = new Rental
                {
                    FilmId = model.FilmId,
                    CopyId = copy.Id,
                    UserId = model.UserId,
                    RentalDate = model.RentalDate,
                    ReturnDate = model.ReturnDate,
                    Note = model.Note
                };

                _rentalRepository.Add(rental);

                copy.Available = false;
                _copyRepository.Update(copy);

                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Return(int id)
        {
            var rental = _rentalRepository.Get(id);
            rental.Returned = true;
            _rentalRepository.Update(rental);

            var copy = _copyRepository.Get(rental.CopyId);
            copy.Available = true;
            _copyRepository.Update(copy);

            return RedirectToAction("Index");
        }
    }
}