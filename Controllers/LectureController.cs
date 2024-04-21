using Microsoft.AspNetCore.Mvc;
using University.Data.Constants;
using University.Data;
using University.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Globalization;
using University.Data.Models;
using Microsoft.VisualBasic;

namespace University.Controllers
{
    public class LectureController : Controller
    {
        private readonly ApplicationDbContext context;

        public LectureController(ApplicationDbContext data)
        {
            context = data;
        }

        public async Task<IActionResult> All()
        {
            if (User.Identity.IsAuthenticated)
            {
                var lectures = await context.Lectures.AsNoTracking()
                    .Select(l => new LectureAllViewModel
                    {
                        Id = l.Id,
                        Topic = l.Topic,
                        Professor = $"{l.Professor.FirstName} {l.Professor.LastName}",
                        Details = l.Details,
                        DateAndTime = l.DateAndTime.ToString(DataConstants.DateFormat),
                        Duration = l.Duration,
                        ProfUserName = l.Professor.User.UserName
                    }).ToListAsync();

                return View(lectures);
            }
            else
            {
                return View("BadRequestError");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = await context.Lectures
                .Where(l => l.Id == id)
                .AsNoTracking()
                .Select(l => new LectureAllViewModel()
                {
                    Id = l.Id,
                    Topic = l.Topic,
                    DateAndTime = l.DateAndTime.ToString(DataConstants.DateFormat),
                    Details = l.Details,
                    Duration = l.Duration,
                    Professor = $"{l.Professor.FirstName} {l.Professor.LastName}",
                    ProfUserName = l.Professor.User.UserName
                }).FirstOrDefaultAsync();

                if (model == null)
                {
                    return RedirectToAction(nameof(Details));
                }

                return View(model);
            }
            else
            {
                return View("BadRequestError");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            if (User.Identity.IsAuthenticated)
            {
                var prof = context.Professors.FirstOrDefault(p => p.UserId == GetUserId());
                if (prof != null)
                {
                    var model = new LectureFormViewModel();

                    return View(model);
                }
                else
                {
                    return View("BadRequestError");
                }
            }
            else
            {
                return View("BadRequestError");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(LectureFormViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                var prof = context.Professors.FirstOrDefault(p => p.UserId == GetUserId());
                if (prof != null)
                {
                    DateTime dateAndTime = DateTime.Now;

                    if (!DateTime.TryParseExact(model.DateAndTime, DataConstants.DateFormat,
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out dateAndTime))
                    {
                        ModelState.AddModelError(nameof(model.DateAndTime), $"Invalid date! Format must be: {DataConstants.DateFormat}");
                    }

                    if (!ModelState.IsValid)
                    {
                        return View(model);
                    }

                    var entity = new Lecture()
                    {
                        Topic = model.Topic,
                        DateAndTime = dateAndTime,
                        Details = model.Details,
                        Duration = model.Duration,
                        ProfessorId = prof.Id
                    };

                    await context.AddAsync(entity);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(All));
                }
                else
                {
                    return View("BadRequestError");
                }
            }
            else
            {
                return View("BadRequestError");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string userId = GetUserId();
            var student = context.Students.FirstOrDefault(p => p.UserId == userId);
            if (User.Identity.IsAuthenticated)
            {
                if (student != null)
                {
                    var model = await context.LecturesParticipants
                        .Where(lp => lp.StudentId == student.Id)
                        .AsNoTracking()
                        .Select(lp => new LectureAllViewModel
                        {
                            Id = lp.LectureId,
                            Topic = lp.Lecture.Topic,
                            DateAndTime = lp.Lecture.DateAndTime.ToString(DataConstants.DateFormat),
                            Details = lp.Lecture.Details,
                            Duration = lp.Lecture.Duration,
                            Professor = $"{lp.Lecture.Professor.FirstName} {lp.Lecture.Professor.LastName}",
                            ProfUserName = lp.Lecture.Professor.User.UserName
                        }).ToListAsync();

                    return View(model);
                }
                else
                {
                    return View("BadRequestError");
                }
            }
            else
            {
                return View("BadRequestError");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            string userId = GetUserId();
            var student = context.Students.Where(s => s.UserId == userId).AsNoTracking().FirstOrDefault();
            if (User.Identity.IsAuthenticated)
            {
                if (student != null)
                {
                    var l = await context.Lectures
                        .Where(l => l.Id == id)
                        .Include(l => l.LectureParticipants)
                        .FirstOrDefaultAsync();

                    if (l == null)
                    {
                        return View("BadRequestError");
                    }

                    var lp = l.LectureParticipants.FirstOrDefault(lp => lp.StudentId == student.Id);

                    if (lp == null)
                    {
                        l.LectureParticipants.Add(new LectureParticipant()
                        {
                            LectureId = id,
                            StudentId = student.Id
                        });

                        await context.SaveChangesAsync();
                    }


                    return RedirectToAction(nameof(Joined));
                }
                else
                {
                    return View("BadRequestError");
                }
            }
            else
            {
                return View("BadRequestError");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string userId = GetUserId();
            var prof = context.Professors.Where(s => s.UserId == userId).AsNoTracking().FirstOrDefault();
            if (User.Identity.IsAuthenticated)
            {
                var l = await context.Lectures.FindAsync(id);

                if (l == null)
                {
                    return View("BadRequestError");
                }

                if (prof == null)
                {
                    return View("BadRequestError");
                }

                var model = new LectureFormViewModel()
                {
                    Topic = l.Topic,
                    DateAndTime = l.DateAndTime.ToString(DataConstants.DateFormat),
                    Details = l.Details,
                    Duration = l.Duration,
                    ProfessorId = prof.Id
                };

                return View(model);
            }
            else
            {
                return View("BadRequestError");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LectureFormViewModel model, int id)
        {
            string userId = GetUserId();
            var prof = context.Professors.Where(s => s.UserId == userId).AsNoTracking().FirstOrDefault();
            if (User.Identity.IsAuthenticated)
            {
                var l = await context.Lectures.FindAsync(id);

                if (l == null)
                {
                    return View("BadRequestError");
                }

                if (prof == null)
                {
                    return View("BadRequestError");
                }

                DateTime dateAndTime = DateTime.Now;

                if (!DateTime.TryParseExact(model.DateAndTime, DataConstants.DateFormat,
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out dateAndTime))
                {
                    ModelState.AddModelError(nameof(model.DateAndTime), $"Invalid date! Format must be: {DataConstants.DateFormat}");
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                l.Topic = model.Topic;
                l.DateAndTime = dateAndTime;
                l.Details = model.Details;
                l.Duration = model.Duration;
                l.ProfessorId = prof.Id;

                await context.SaveChangesAsync();
                return RedirectToAction(nameof(All));
            }
            else
            {
                return View("BadRequestError");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string userId = GetUserId();
            var prof = context.Professors.Where(s => s.UserId == userId).AsNoTracking().FirstOrDefault();
            if (User.Identity.IsAuthenticated)
            {
                var l = await context.Lectures.FindAsync(id);

                if (l == null)
                {
                    return View("BadRequestError");
                }

                if (prof == null)
                {
                    return View("BadRequestError");
                }

                var model = new LectureAllViewModel()
                {
                    Topic = l.Topic,
                    DateAndTime = l.DateAndTime.ToString(DataConstants.DateFormat)
                };

                return View(model);
            }
            else
            {
                return View("BadRequestError");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(LectureAllViewModel model, int id)
        {
            string userId = GetUserId();
            var prof = context.Professors.Where(s => s.UserId == userId).AsNoTracking().FirstOrDefault();
            if (User.Identity.IsAuthenticated)
            {
                var l = await context.Lectures.FindAsync(id);

                if (l == null)
                {
                    return View("BadRequestError");
                }

                if (prof == null)
                {
                    return View("BadRequestError");
                }

                var lp = context.LecturesParticipants.FirstOrDefault(sp => sp.LectureId == id);
                if (lp != null)
                {
                    context.Remove(lp);
                }
                context.Remove(l);
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
