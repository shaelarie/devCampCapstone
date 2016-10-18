using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyTrainer.Models;

namespace MyTrainer.Controllers
{
    public class BasicMealPlansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BasicMealPlans
        public ActionResult Index()
        {
            return View(db.BasicDb.ToList());
        }

        // GET: BasicMealPlans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BasicMealPlan basicMealPlan = db.BasicDb.Find(id);
            if (basicMealPlan == null)
            {
                return HttpNotFound();
            }
            return View(basicMealPlan);
        }

        // GET: BasicMealPlans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BasicMealPlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProteinIntake,CarbIntake,FatIntake")] BasicMealPlan basicMealPlan)
        {
            if (ModelState.IsValid)
            {
                db.BasicDb.Add(basicMealPlan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(basicMealPlan);
        }

        // GET: BasicMealPlans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BasicMealPlan basicMealPlan = db.BasicDb.Find(id);
            if (basicMealPlan == null)
            {
                return HttpNotFound();
            }
            return View(basicMealPlan);
        }

        // POST: BasicMealPlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProteinIntake,CarbIntake,FatIntake")] BasicMealPlan basicMealPlan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(basicMealPlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(basicMealPlan);
        }

        // GET: BasicMealPlans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BasicMealPlan basicMealPlan = db.BasicDb.Find(id);
            if (basicMealPlan == null)
            {
                return HttpNotFound();
            }
            return View(basicMealPlan);
        }

        // POST: BasicMealPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BasicMealPlan basicMealPlan = db.BasicDb.Find(id);
            db.BasicDb.Remove(basicMealPlan);
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
