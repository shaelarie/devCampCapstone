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
    public class MealPlansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MealPlans
        [Authorize]
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            MealPlan mealPlan = db.MealDb.FirstOrDefault(x => x.Id == currentUser.Id);
            var findUserPlan = db.MealDb.Where(x => x.Id == currentUser.MealPlanId).Select(x => x);
            return View(findUserPlan);
        }

        // GET: MealPlans/Details/5
        [Authorize]
        public ActionResult Details()
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            int id = currentUser.Id;
            MealPlan mealPlan = db.MealDb.Find(id);
            return View(mealPlan);
        }

        // GET: MealPlans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MealPlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Basic,Vegetarian,Vegan")] MealPlan mealPlan)
        {
            if (ModelState.IsValid)
            {
                db.MealDb.Add(mealPlan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mealPlan);
        }

        // GET: MealPlans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MealPlan mealPlan = db.MealDb.Find(id);
            if (mealPlan == null)
            {
                return HttpNotFound();
            }
            return View(mealPlan);
        }

        // POST: MealPlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MealPlanDetails,ProteinIntake,CarbIntake,FatIntake,Meal1,Snack1,Meal2,Snack2,Meal3")] MealPlan mealPlan)
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            currentUser.MealPlanId = mealPlan.Id;
            if (ModelState.IsValid)
            {
                db.Entry(mealPlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "Users");
            }
            return View(mealPlan);
        }

        
        // GET: MealPlans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MealPlan mealPlan = db.MealDb.Find(id);
            if (mealPlan == null)
            {
                return HttpNotFound();
            }
            return View(mealPlan);
        }

        // POST: MealPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MealPlan mealPlan = db.MealDb.Find(id);
            db.MealDb.Remove(mealPlan);
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
