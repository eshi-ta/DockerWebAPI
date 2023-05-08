using BookReadingEvent.Data;
using BookReadingEvent.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace BookReadingEvent.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly ApplicationDbcontext _db;



        public EventController(ApplicationDbcontext db)
        {
            _db = db;
        }



        public IActionResult Index()
        {
            return View();
        }



        public IActionResult CreateEvent()
        {



            return View();
        }
        [HttpPost]
        public IActionResult CreateEvent(Event obj)
        {
            if (ModelState.IsValid)
            {
                _db.Events.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(obj);
        }
        public IActionResult MyEvents()
        {
            List<Event> eventsFromDb = _db.Events.ToList();
            return View(eventsFromDb);
        }
        public IActionResult EventDetails(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Event obj = _db.Events.FirstOrDefault(e => e.Id == Id);
            if (obj == null)
            {
                return NotFound();
            }
            List<Comment> comments = _db.Comments.ToList();
            EventCommentViewModel eventCommentViewModel = new EventCommentViewModel()
            {
                Event = obj,
                Comment = new Comment(),
                Comments = comments



            };
            return View(eventCommentViewModel);
        }
        public IActionResult MyInvitations()
        {
            var list = _db.Events.ToList();
            return View(list);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var eventFromDb = _db.Events.Find(id);
            if (eventFromDb == null)
            {
                return NotFound();
            }
            return View(eventFromDb);



        }
        [HttpPost]



        public IActionResult Edit(Event obj)
        {
            if (ModelState.IsValid)
            {
                _db.Events.Update(obj);
                _db.SaveChanges();
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("ListAllEvents", "Event");
                }
                return RedirectToAction("MyEvents", "Event");
            }
            return View(obj);
        }



        public IActionResult ListAllEvents()
        {
            var eventList = _db.Events.ToList();
            return View(eventList);
        }



        [HttpPost]
        public IActionResult AddComment(EventCommentViewModel viewModel)
        {
            Comment comment = new Comment()
            {
                EventId = viewModel.Event.Id,
                CommentText = viewModel.Comment.CommentText,
                Author = User.Identity.Name,
                CreatedDate = DateTime.Now,
            };
            _db.Comments.Add(comment);
            _db.SaveChanges();

            return RedirectToAction("EventDetails", "Event", new { Id = viewModel.Event.Id });
        }
    }
}