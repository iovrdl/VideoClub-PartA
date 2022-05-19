using System.Linq;
using System.Web.Mvc;
using PagedList;
using VideoClub.Data.Entities;
using VideoClub.Data.Services;
using VideoClub.Web.Models;

namespace VideoClub.Web.Controllers
{
    [Authorize]
    public class FilmsController : Controller
    {
        private readonly IFilmRepository _filmRepository;
        private readonly ICopyRepository _copyRepository;

        public FilmsController(IFilmRepository filmRepository, ICopyRepository copyRepository)
        {
            _filmRepository = filmRepository;
            _copyRepository = copyRepository;
        }
        
        public ActionResult Index(FilmsBindModel bindModel, string searchString, int? page)
        {
            var films = _filmRepository.GetAll().OrderBy(film => film.Id);

            if (bindModel.Genre != null)
            {
                films = films.Where(film => film.Genre == bindModel.Genre).OrderBy(film => film.Id);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                films = films.Where(film => film.Title.Contains(searchString)).OrderBy(film => film.Id);
            }

            const int pageSize = 3;
            var pageNumber = page ?? 1;

            var model = new FilmsBindModel
            {
                Films = films.ToPagedList(pageNumber, pageSize)
            };
            
            if (bindModel.Genre != null)
            {
                model.Genre = bindModel.Genre;
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Rent(int id)
        {
            return RedirectToAction("Create", "Rentals", new { filmId = id });
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = _filmRepository.Get(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var model = new CreateFilmBindModel
            {
                Film = new Film(),
                Copies = 1
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(CreateFilmBindModel bindModel)
        {
            if (ModelState.IsValid)
            {
                _filmRepository.Add(bindModel.Film);

                for (var i = 0; i < bindModel.Copies; i++)
                {
                    _copyRepository.Add(new Copy
                    {
                        FilmId = bindModel.Film.Id,
                        Film = bindModel.Film,
                        Available = true
                    });
                }

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}