using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyTrainer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace MyTrainer.Controllers
{

    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Users
        [Authorize]
        public ActionResult Index([Bind(Include = "Id,Weight,HeightFt,HeightIn,LoginId")] User user)
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            var scheduleUserId = db.ScheduleDb.Where(x => x.UserId == currentUser.Id).Select(x => x.Id);
                if(currentUser.Id.ToString() != scheduleUserId.ToString())
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if(user == null || userId == null)
                {
                try
                {
                    return RedirectToAction("Index", currentUser);
                }
                catch
                {
                    return RedirectToAction("Login", "Account");
                }
                }
            JavaScriptSerializer serialize = new JavaScriptSerializer();
            var UserEvents = db.ScheduleDb.Where(x => x.UserId == currentUser.Id).Select(x => x).
            Include(x => x.User).ToList();
            var ListToSerialize = db.ScheduleDb.Where(x => x.UserId == currentUser.Id).ToList().
                GroupBy(x => x.eventId).Select(x => x.First()).ToList();
            string SerializedList = SerializeCalendarEventList(ListToSerialize);
            
            foreach(UserSchedule item in UserEvents)
            {
                item.data = SerializedList;
                db.SaveChanges();
            }
            
            return View(UserEvents);
            //var view = db.ScheduleDb.Where()
            //return View(db.UserDb.Where(x => x.LoginId == currentUser.LoginId).Select(x => x).ToList());

        }

        // GET: Users/Details/5
        public ActionResult Details()
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            return RedirectToAction("Index", currentUser);
        }
        // GET: Users/Create
        public ActionResult Create()
        {

            ViewBag.LoginId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Weight,HeightFt,HeightIn,LoginId")] User user)
        {
            if (ModelState.IsValid)
            {
                db.UserDb.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index", user);
            }

            ViewBag.EmailId = new SelectList(db.Users, "Id", "Email", user.LoginId);
            return View(user);
        }

        // GET: Users/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            User user = db.UserDb.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmailId = new SelectList(db.Users, "Id", "Email", user.LoginId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Weight,HeightFt,HeightIn,LoginId")] User user)
        {
            var schedule = db.ScheduleDb.Where(x => x.UserId == user.Id).Select(x => x);
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", user);
            }
            
            ViewBag.EmailId = new SelectList(db.Users, "Id", "Email", User);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.UserDb.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View("Index", user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.UserDb.Find(id);
            db.UserDb.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //public ActionResult CreateEvent()
        //{

        //}
        public string SerializeCalendarEventList(List<UserSchedule> EventList)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(EventList);
            return json;

        }
        public void SaveEvent(UserSchedule Event)
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            Event.startDate = Convert.ToDateTime(Event.start + " " + Event.startTime);
            Event.endDate = Convert.ToDateTime(Event.endDate + " " + Event.endTime);
            Event.UserId = currentUser.Id;
            Event.eventId = (Event.title + Event.startDate + Event.startTime);
            db.ScheduleDb.Add(Event);
            db.SaveChanges();
            Response.Redirect("http://localhost:3011/Users/Index");
        }

        public UserSchedule getEvent(string eventID)
        {
            UserSchedule schedule = new UserSchedule();
            schedule = db.ScheduleDb.Where(x => x.eventId == eventID).First();
            return schedule;
        }

        public void editEvents(UserSchedule events)
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            UserSchedule eventToEdit = new UserSchedule();
            try
            {
                eventToEdit = getEvent(events.eventId);
            }
            catch(NullReferenceException e)
            {
                throw new Exception("This entry does not exist", e);
            }
            eventToEdit.UserId = currentUser.Id;
            eventToEdit.title = events.title;
            eventToEdit.eventDescription = events.eventDescription;
            eventToEdit.start = events.start;
            eventToEdit.startTime = events.startTime;
            eventToEdit.end = events.end;
            eventToEdit.endTime = events.endTime;
            eventToEdit.background = events.background;
            eventToEdit.startDate = Convert.ToDateTime(events.startDate + " " + events.startTime);
            eventToEdit.endDate = Convert.ToDateTime(events.endDate + " " + events.endDate);
            eventToEdit.eventId = ResetEventID(events);
            db.SaveChanges();
            Response.Redirect("http://localhost:3011/Users/Index");
        }
        public string ResetEventID(UserSchedule Event)
        {
            string eventID = Event.title + Event.UserId + Event.startDate + Event.startTime;
            return eventID;
        }

        public void DeleteEvent(UserSchedule Events)
        {
            UserSchedule eventToDelete = getEvent(Events.eventId);
            db.ScheduleDb.Remove(eventToDelete);
            db.SaveChanges();
            Response.Redirect("http://localhost:3011/Users/Index");

        }

        private UserSchedule DeserializeObj(string eventRow)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(UserSchedule));
            FileStream stream = new FileStream(eventRow, FileMode.Open);
            XmlReader reader = XmlReader.Create(stream);
            UserSchedule events = new UserSchedule();
            events = (UserSchedule)serializer.Deserialize(reader);
            stream.Close();
            return events;
        }

        public List<string> SplitString(string list)
        {
            List<string> newList = list.Replace(" ", " ").Split(',').ToList();
            return newList;
        }

        public List<UserSchedule> copyEventList(List<UserSchedule> listOfEvents)
        {
            List<UserSchedule> tempList = new List<UserSchedule>();
            for(int i = 0; i < listOfEvents.Count; i++)
            {
                tempList.Add(listOfEvents[i]);
                tempList[i].editable = false;
                tempList[i].background = "grey";
            }
            return tempList;
        }

    }
}
