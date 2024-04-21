using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using University.Data;
using University.Data.Constants;
using University.Data.Models;
using University.Models;

namespace University.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext context;

        public EventController(ApplicationDbContext data)
        {
            context = data;
        }

        public async Task<IActionResult> All()
        {
            if (User.Identity.IsAuthenticated)
            {
                var events = await context.Events.AsNoTracking()
                    .Select(s => new EventAllViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Description = s.Description,
                        DateTime = s.DateTime.ToString(DataConstants.DateFormat)
                    }).ToListAsync();

                return View(events);
            }
            else
            {
                return View("BadRequestError");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = GetUserId();

                var model = await context.EventsParticipants
                    .Where(sp => sp.UserId == userId)
                    .AsNoTracking()
                    .Select(sp => new EventAllViewModel
                    {
                        Id = sp.Event.Id,
                        Name = sp.Event.Name,
                        Description = sp.Event.Description,
                        DateTime = sp.Event.DateTime.ToString(DataConstants.DateFormat)
                    }).ToListAsync();

                return View(model);
            }
            else
            {
                return View("BadRequestError");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var e = await context.Events
                    .Where(s => s.Id == id)
                    .Include(s => s.EventParticipants)
                    .FirstOrDefaultAsync();

                if (e == null)
                {
                    return View("BadRequestError");
                }

                string userId = GetUserId();

                var ep = e.EventParticipants.FirstOrDefault(ep => ep.UserId == userId);

                if (ep == null)
                {
                    e.EventParticipants.Add(new EventParticipant()
                    {
                        EventId = id,
                        UserId = userId
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
