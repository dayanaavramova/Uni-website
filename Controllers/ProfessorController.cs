using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using University.Data;
using University.Data.Constants;
using University.Data.Models;
using University.Models;

namespace University.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly ApplicationDbContext context;
        private int count = 0;

        public ProfessorController(ApplicationDbContext data)
        {
            context = data;
        }

        public async Task<IActionResult> All()
        {
            if (User.Identity.IsAuthenticated)
            {
                var professors = await context.Professors.AsNoTracking()
                .Select(p => new ProfessorViewModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Rating = p.Rating,
                    ProfUserName = p.User.UserName
                }).ToListAsync();

                return View(professors);
            }
            else
            {
                return View("BadRequestError");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Rate(int id)
        {
            string userId = GetUserId();
            var student = context.Students.Where(s => s.UserId == userId).AsNoTracking().FirstOrDefault();
            if (User.Identity.IsAuthenticated)
            {
                var p = await context.Professors.FindAsync(id);

                if (p == null)
                {
                    return View("BadRequestError");
                }

                if (student == null)
                {
                    return View("BadRequestError");
                }

                var model = new ProfRateFormViewModel()
                {
                    Rating = p.Rating
                };

                return View(model);
            }
            else
            {
                return View("BadRequestError");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Rate(ProfRateFormViewModel model, int id)
        {
            string userId = GetUserId();
            var student = context.Students.Where(s => s.UserId == userId).AsNoTracking().FirstOrDefault();
            
            if (User.Identity.IsAuthenticated)
            {
                var p = await context.Professors.FindAsync(id);

                if (p == null)
                {
                    return View("BadRequestError");
                }

                if (student == null)
                {
                    return View("BadRequestError");
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                count++;
                p.Rating = p.Rating + model.Rating / count;

                await context.SaveChangesAsync();
                return RedirectToAction(nameof(All));
            }
            else
            {
                return View("BadRequestError");
            }
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
