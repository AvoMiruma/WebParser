using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parser.Domain;
using Parser.Models;
using Parser.Persistance;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Parser.Controllers
{
    public class HomeController : Controller
    {
        private VacancyDbContext db;
        public HomeController(VacancyDbContext context)
        {
            db = context;
        }


        public async Task<IActionResult> Index()
        {
            List<Vacancy> vac = new List<Vacancy>();
            vac = await db.Vacances.ToListAsync();
            return View(vac);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create(Vacancy user)
        {
            List<Vacancy> VacancyList = new List<Vacancy>();
            List<Vacancy> temp = new List<Vacancy>();

            for (int j = 1; j < 28; j++)
            {
                string responce = Page.GetPage("https://www.work.ua/jobs-it-it/?page=" + j);
                VacancyList.AddRange(Page.ParsPage(responce));

            }

            foreach (var model in VacancyList)
            {
                if (model.Details.Contains("Неповна зайнятість"))
                {
                    temp.AddRange(Page.Add(model));
                }
                else
                {
                    db.Vacances.AddRange(model);
                }
            }
            foreach (var model in temp)
            {
                db.Vacances.AddRange(model);
            }

            db.Vacances.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
