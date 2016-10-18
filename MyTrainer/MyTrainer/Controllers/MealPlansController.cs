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
            var findUserPlan = db.MealDb.Where(x => x.Id == currentUser.MealPlanId).Select(x => x);
            return View(findUserPlan);
        }

        // GET: MealPlans/Details/5
        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MealPlan mealPlan = db.MealDb.Find(id);
            if(mealPlan.MealPlanType == "Basic")
            {
                return RedirectToAction("Index", "BasicMealPlans");
            }
            if(mealPlan.MealPlanType == "Vegetarian")
            {
                return RedirectToAction("Index", "VegetarianMealPlans");
            }
            if(mealPlan.MealPlanType == "Vegan")
            {
                return RedirectToAction("Index", "VeganMealPlans");
            }
            if (mealPlan == null)
            {
                return HttpNotFound();
            }
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
        public ActionResult Edit([Bind(Include = "Id,MealPlanType,BasicId,VegetarianId,VeganId")] MealPlan mealPlan)
        {
            if (ModelState.IsValid)
            {
                if(mealPlan.MealPlanType == "Basic"){
                    mealPlan.VeganId = null;
                    mealPlan.VegetarianId = null;
                }
                if (mealPlan.MealPlanType == "Vegetarian")
                {
                    mealPlan.BasicId = null;
                    mealPlan.VeganId = null;
                }
                if (mealPlan.MealPlanType == "Vegan")
                {
                    mealPlan.VegetarianId = null;
                    mealPlan.BasicId = null;
                }
                db.Entry(mealPlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
