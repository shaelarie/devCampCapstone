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

namespace MyTrainer.Controllers
{
    public class GoalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Goals
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            var userGoal = db.GoalDb.Where(x => x.Id == currentUser.Id).Select(x => x).ToList();
            return View(userGoal);
        }

        // GET: Goals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Goals goals = db.GoalDb.Find(id);
            if (goals == null)
            {
                return HttpNotFound();
            }
            return View(goals);
        }

        // GET: Goals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Goals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LoseWeight,GainMuscle,Maintain")] Goals goals)
        {
            if (ModelState.IsValid)
            {
                db.GoalDb.Add(goals);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(goals);
        }

        // GET: Goals/Edit/5
        public ActionResult Edit()
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            int id = currentUser.Id;
            Goals goals = db.GoalDb.Find(id);
            if (goals == null)
            {
                return HttpNotFound();
            }
            return View(goals);
        }

        // POST: Goals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserGoal")] Goals goals)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goals).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit","MealPlans", new { id = goals.Id});
            }
            return View(goals);
        }

        // GET: Goals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Goals goals = db.GoalDb.Find(id);
            if (goals == null)
            {
                return HttpNotFound();
            }
            return View(goals);
        }

        // POST: Goals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Goals goals = db.GoalDb.Find(id);
            db.GoalDb.Remove(goals);
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
