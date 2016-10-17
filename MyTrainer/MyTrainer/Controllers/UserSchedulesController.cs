using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using MyTrainer.Models;
using System.Web.Script.Serialization;
using System.Globalization;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.IO;
using Microsoft.AspNet.Identity.Owin;

namespace MyTrainer.Controllers
{
    public class UserSchedulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            try
            {
                var eventList = db.ScheduleDb.Where(g => g.UserId == currentUser.Id).
                ToList().GroupBy(x => x.eventId).Select(y => y.First()).ToList();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string jsonEvents = SerializeCalendarEventList(eventList);
                return View(new UserSchedule() { data = jsonEvents });
            }
            catch
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public void SaveEvent(UserSchedule data)
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            data.startDate = Convert.ToDateTime(data.start + " " + data.startTime);
            data.endDate = Convert.ToDateTime(data.end + " " + data.endTime);
            data.UserId = currentUser.Id;
            data.eventId = (data.title + data.UserId + data.startDate + data.startTime);
            db.ScheduleDb.Add(data);
            db.SaveChanges();
            Response.Redirect("http://localhost:3011/UserSchedules/Index");
        }

        public UserSchedule GetEvents(string eventID)
        {
            UserSchedule existingEvent = new UserSchedule();
            existingEvent = db.ScheduleDb.Where(g => g.eventId == eventID).First();
            return existingEvent;
        }



        public void UpdateEvent(UserSchedule data)
        {
            UserSchedule oldEvent = new UserSchedule();
            try
            {
                oldEvent = GetEvents(data.eventId);
            }
            catch (NullReferenceException e)
            {
                throw new Exception("That event does not exist.", e);
            }

            oldEvent.title = data.title;
            oldEvent.eventDescription = data.eventDescription;
            oldEvent.startDate = data.startDate;
            oldEvent.startTime = data.startTime;
            oldEvent.endDate = data.endDate;
            oldEvent.endTime = data.endTime;
            oldEvent.background = data.background;
            oldEvent.startDate = Convert.ToDateTime(data.start + " " + data.startTime);
            oldEvent.endDate = Convert.ToDateTime(data.end + " " + data.endTime);
            oldEvent.eventId = ResetEventID(data);
            db.SaveChanges();
            Response.Redirect("http://localhost:3011/UserSchedules/Index");
        }

        public string ResetEventID(UserSchedule myEvent)
        {
            string eventID = myEvent.title + myEvent.UserId + myEvent.startDate + myEvent.startTime;
            return eventID;
        }

        public void DeleteEvent(UserSchedule data)
        {
            UserSchedule oldEvent = GetEvents(data.eventId);
            db.ScheduleDb.Remove(oldEvent);
            db.SaveChanges();
            Response.Redirect("http://localhost:3011/UserSchedules/Index");
        }
        public string SerializeCalendarEventList(List<UserSchedule> EventList)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(EventList);
            return json;

        }
        private UserSchedule DeserializeObject(string dataRow)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(UserSchedule));
            FileStream myStream = new FileStream(dataRow, FileMode.Open);
            XmlReader myReader = XmlReader.Create(myStream);
            UserSchedule myEvent = new UserSchedule();

            myEvent = (UserSchedule)serializer.Deserialize(myReader);
            myStream.Close();
            return myEvent;
        }

        public List<string> SplitStringToList(string list)
        {
            List<string> NewList = list.Replace(" ", "").Split(',').ToList();
            return NewList;
        }

        public List<UserSchedule> CopyEventList(List<UserSchedule> EventList)
        {
            List<UserSchedule> tempList = new List<UserSchedule>();
            for (int k = 0; k < EventList.Count; k++)
            {
                tempList.Add(EventList[k]);
                tempList[k].editable = false;
                tempList[k].background = "gray";
            }
            return tempList;
        }

 
    }
}