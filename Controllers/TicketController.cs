using BiletSatis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiletSatis.Controllers
{
    public class TicketController : Controller
    {
        private TicketDbContext _dbContext;
        public TicketController(TicketDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            FillMovie fillMovie = new FillMovie();
            fillMovie.Run(_dbContext);

            var movies = _dbContext.Movies.ToList();
            return View(movies);
        }

        [HttpGet]
        public IActionResult Sale(int id)
        {
            FillSalePage(id);
            return View();
        }

        private void FillSalePage(int id)
        {
            var movie = _dbContext.Movies.First(x => x.Id == id);

            ViewBag.MovieTitle = movie.Title;
            ViewBag.MoviePoster = movie.Poster;
            ViewBag.MovieOverview = movie.Overview;
            ViewBag.HataVarMi = false;

            //select * from Sessions where MovieId = 5
            ViewBag.Sessions = _dbContext.Sessions.Include("Saloon").Where(x => x.MovieId == id).ToList();
        }

        [HttpPost]
        public IActionResult Sale(Ticket model)
        {
            //toplam satılan bilet + bu satılan bilet sayısı
            //salonun kapasitesi

            //seans bilgisi
            var session = _dbContext.Sessions.Include("Saloon").First(x => x.Id == model.SessionId);

            //select sum(Amount) from Tickets where SessionId = 1
            var toplamSatilanBilet = _dbContext.Tickets.Where(x => x.SessionId == model.SessionId).Sum(x => x.Amount);

            if ((toplamSatilanBilet + model.Amount) <= session.Saloon.Capacity)
            {
                //dbye ticketi kaydet
                _dbContext.Tickets.Add(model);
                _dbContext.SaveChanges();

                return RedirectToAction("List");
            }
            else
            {
                ViewBag.HataVarMi = true;
                FillSalePage(session.MovieId);

                return View();
            }
        }

        public IActionResult List()
        {
            //select * from Tickets
            var list = _dbContext.Tickets.Include("Session.Movie").Include("Session.Saloon").ToList();
            return View(list);
        }
    }
}
