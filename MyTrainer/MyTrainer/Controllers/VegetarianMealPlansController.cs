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
    public class VegetarianMealPlansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VegetarianMealPlans
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            MealPlan findPlan = db.MealDb.FirstOrDefault(x => x.Id == currentUser.MealPlanId);
            var plan = db.VegetarianDb.Where(x => x.Id == findPlan.VegetarianId).Select(x => x).ToList();
            return View(plan);
        }

        // GET: VegetarianMealPlans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VegetarianMealPlan vegetarianMealPlan = db.VegetarianDb.Find(id);
            if (vegetarianMealPlan == null)
            {
                return HttpNotFound();
            }
            return View(vegetarianMealPlan);
        }

        // GET: VegetarianMealPlans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VegetarianMealPlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProteinIntake,CarbIntake,FatIntake")] VegetarianMealPlan vegetarianMealPlan)
        {
            if (ModelState.IsValid)
            {
                db.VegetarianDb.Add(vegetarianMealPlan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vegetarianMealPlan);
        }

        // GET: VegetarianMealPlans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VegetarianMealPlan vegetarianMealPlan = db.VegetarianDb.Find(id);
            if (vegetarianMealPlan == null)
            {
                return HttpNotFound();
            }
            return View(vegetarianMealPlan);
        }

        // POST: VegetarianMealPlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProteinIntake,CarbIntake,FatIntake")] VegetarianMealPlan vegetarianMealPlan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vegetarianMealPlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vegetarianMealPlan);
        }

        // GET: VegetarianMealPlans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VegetarianMealPlan vegetarianMealPlan = db.VegetarianDb.Find(id);
            if (vegetarianMealPlan == null)
            {
                return HttpNotFound();
            }
            return View(vegetarianMealPlan);
        }

        // POST: VegetarianMealPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VegetarianMealPlan vegetarianMealPlan = db.VegetarianDb.Find(id);
            db.VegetarianDb.Remove(vegetarianMealPlan);
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
