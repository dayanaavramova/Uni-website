using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using University.Data;
using University.Data.Models;
using University.Models;

namespace University.Controllers
{
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext context;

        public RoleController(ApplicationDbContext data)
        {
            context = data;
        }

        [HttpGet]
        public IActionResult Role()
        {
            var prof = context.Professors.FirstOrDefault(p => p.UserId == GetUserId());
            var student = context.Students.FirstOrDefault(p => p.UserId == GetUserId());
            if (prof != null || student != null)
            {
                return RedirectToAction(nameof(AlreadyHasRole));
            }
            else if (!User.Identity.IsAuthenticated)
            {
                return View("BadRequestError");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Role(string role)
        {
            var prof = context.Professors.FirstOrDefault(p => p.UserId == GetUserId());
            var student = context.Students.FirstOrDefault(p => p.UserId == GetUserId());
            if (prof != null || student != null)
            {
                return RedirectToAction(nameof(AlreadyHasRole));
            }
            else if (!User.Identity.IsAuthenticated)
            {
                return View("BadRequestError");
            }
            else
            {
                if (role == "professor")
                {
                    return RedirectToAction("ProfessorForm", "Role");
                }
                else if (role == "student")
                {
                    return RedirectToAction("StudentForm", "Role");
                }
                else
                {
                    throw new Exception("An unexpected error occurred.");
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> ProfessorForm()
        {
            var prof = context.Professors.FirstOrDefault(p => p.UserId == GetUserId());
            var student = context.Students.FirstOrDefault(p => p.UserId == GetUserId());
            if (prof != null || student != null)
            {
                return RedirectToAction(nameof(AlreadyHasRole));
            }
            else if (!User.Identity.IsAuthenticated)
            {
                return View("BadRequestError");
            }
            else
            {
                var model = new ProfessorFormViewModel();
                model.Subjects = await GetSubjects();
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ProfessorForm(ProfessorFormViewModel model)
        {
            var prof = context.Professors.FirstOrDefault(p => p.UserId == GetUserId());
            var student = context.Students.FirstOrDefault(p => p.UserId == GetUserId());
            if (prof != null || student != null)
            {
                return RedirectToAction(nameof(AlreadyHasRole));
            }
            else if (!User.Identity.IsAuthenticated)
            {
                return View("BadRequestError");
            }
            else
            {

                if (!ModelState.IsValid)
                {
                    model.Subjects = await GetSubjects();
                    return View(model);
                }

                var entity = new Professor()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    SubjectId = model.SubjectId,
                    UserId = GetUserId()
                };

                await context.AddAsync(entity);
                await context.SaveChangesAsync();
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }

        [HttpGet]
        public IActionResult StudentForm()
        {
            var prof = context.Professors.FirstOrDefault(p => p.UserId == GetUserId());
            var student = context.Students.FirstOrDefault(p => p.UserId == GetUserId());
            if (prof != null || student != null)
            {
                return RedirectToAction(nameof(AlreadyHasRole));
            }
            else if (!User.Identity.IsAuthenticated)
            {
                return View("BadRequestError");
            }
            else
            {
                var model = new StudentFormViewModel();
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> StudentForm(StudentFormViewModel model)
        {
            var prof = context.Professors.FirstOrDefault(p => p.UserId == GetUserId());
            var student = context.Students.FirstOrDefault(p => p.UserId == GetUserId());
            if (prof != null || student != null)
            {
                return RedirectToAction(nameof(AlreadyHasRole));
            }
            else if (!User.Identity.IsAuthenticated)
            {
                return View("BadRequestError");
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var entity = new Student()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Year = model.Year,
                    UserId = GetUserId()
                };

                await context.AddAsync(entity);
                await context.SaveChangesAsync();
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }

        public IActionResult AlreadyHasRole()
        {
            return View();
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        private async Task<IEnumerable<SubjectViewModel>> GetSubjects()
        {
            return await context.Subjects.AsNoTracking()
                .Select(t => new SubjectViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToListAsync();
        }
    }
}
