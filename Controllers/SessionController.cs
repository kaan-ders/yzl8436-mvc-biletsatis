using BiletSatis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiletSatis.Controllers
{
    public class SessionController : Controller
    {
        private TicketDbContext _dbContext;
        public SessionController(TicketDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var sessions = _dbContext.Sessions.Include("Movie").Include("Saloon").ToList();
            return View(sessions);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Movies = _dbContext.Movies.OrderBy(x=> x.Title).ToList();
            ViewBag.Saloons = _dbContext.Saloons.Where(x => x.IsDeleted == false).OrderBy(x=> x.No).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Add(Session model)
        {
            //dbye kaydet
            _dbContext.Sessions.Add(model);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
