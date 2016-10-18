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
    public class VeganMealPlansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VeganMealPlans
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            MealPlan findPlan = db.MealDb.FirstOrDefault(x => x.Id == currentUser.MealPlanId);
            var plan = db.VeganDb.Where(x => x.Id == findPlan.VeganId).Select(x => x).ToList();
            return View(plan);
        }

        // GET: VeganMealPlans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VeganMealPlan veganMealPlan = db.VeganDb.Find(id);
            if (veganMealPlan == null)
            {
                return HttpNotFound();
            }
            return View(veganMealPlan);
        }

        // GET: VeganMealPlans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VeganMealPlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProteinIntake,CarbIntake,FatIntake")] VeganMealPlan veganMealPlan)
        {
            if (ModelState.IsValid)
            {
                db.VeganDb.Add(veganMealPlan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(veganMealPlan);
        }

        // GET: VeganMealPlans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VeganMealPlan veganMealPlan = db.VeganDb.Find(id);
            if (veganMealPlan == null)
            {
                return HttpNotFound();
            }
            return View(veganMealPlan);
        }

        // POST: VeganMealPlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProteinIntake,CarbIntake,FatIntake")] VeganMealPlan veganMealPlan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(veganMealPlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(veganMealPlan);
        }

        // GET: VeganMealPlans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VeganMealPlan veganMealPlan = db.VeganDb.Find(id);
            if (veganMealPlan == null)
            {
                return HttpNotFound();
            }
            return View(veganMealPlan);
        }

        // POST: VeganMealPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VeganMealPlan veganMealPlan = db.VeganDb.Find(id);
            db.VeganDb.Remove(veganMealPlan);
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
