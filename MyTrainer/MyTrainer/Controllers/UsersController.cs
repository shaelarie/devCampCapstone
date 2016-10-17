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
            var UserEvents = db.UserDb.Where(x => x.LoginId == currentUser.LoginId).Select(x => x).ToList();
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

    }
}
