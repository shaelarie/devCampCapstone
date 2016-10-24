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
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            MealPlan mealPlan = db.MealDb.FirstOrDefault(x => x.Id == currentUser.Id);
            
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

        [HttpPost]
        public ActionResult saveToMeal1(string name, string cals, string protein, string fat, string carbs, string serving)
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            MealPlan meal = db.MealDb.FirstOrDefault(x => x.Id == currentUser.Id);
            Meal1 meal1 = new Meal1
            {
                MealPlanId = meal.Id,
                FoodItem = Convert.ToString(name),
                servingSize = Convert.ToDouble(serving),
                calories = Convert.ToDouble(cals),
                protein = Convert.ToDouble(protein),
                fat = Convert.ToDouble(fat),
                carbs = Convert.ToDouble(carbs)
            };
            var currentCals = meal.CaloriesAdded;
            double calsAdded = Convert.ToDouble(cals);
            var currentCarbs = meal.CarbsAdded;
            double carbsAdded = Convert.ToDouble(carbs);
            var currentFat = meal.FatAdded;
            double FatAdded = Convert.ToDouble(fat);
            var currentProtein = meal.ProteinAdded;
            double ProteinAdded = Convert.ToDouble(protein);
            meal.FatAdded = Convert.ToDouble(currentFat) + FatAdded;
            meal.CarbsAdded = Convert.ToDouble(currentCarbs) + carbsAdded;
            meal.CaloriesAdded = Convert.ToDouble(currentCals) + calsAdded;
            meal.ProteinAdded = Convert.ToDouble(currentProtein) + ProteinAdded;
            db.Meal1Db.Add(meal1);
            db.SaveChanges();
            Object[] data = new Object[4];
            data[0] = meal.CaloriesAdded;
            data[1] = meal.ProteinAdded;
            data[2] = meal.FatAdded;
            data[3] = meal.CarbsAdded;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult saveToSnack1(string name, string cals, string protein, string fat, string carbs, string serving)
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            MealPlan meal = db.MealDb.FirstOrDefault(x => x.Id == currentUser.Id);
            Snack1 snack1 = new Snack1
            {
                MealPlanId = meal.Id,
                FoodItem = Convert.ToString(name),
                servingSize = Convert.ToDouble(serving),
                calories = Convert.ToDouble(cals),
                protein = Convert.ToDouble(protein),
                fat = Convert.ToDouble(fat),
                carbs = Convert.ToDouble(carbs)
            };
            var currentCals = meal.CaloriesAdded;
            double calsAdded = Convert.ToDouble(cals);
            var currentCarbs = meal.CarbsAdded;
            double carbsAdded = Convert.ToDouble(carbs);
            var currentFat = meal.FatAdded;
            double FatAdded = Convert.ToDouble(fat);
            var currentProtein = meal.ProteinAdded;
            double ProteinAdded = Convert.ToDouble(protein);
            meal.FatAdded = Convert.ToDouble(currentFat) + FatAdded;
            meal.CarbsAdded = Convert.ToDouble(currentCarbs) + carbsAdded;
            meal.CaloriesAdded = Convert.ToDouble(currentCals) + calsAdded;
            meal.ProteinAdded = Convert.ToDouble(currentProtein) + ProteinAdded;
            db.Snack1Db.Add(snack1);
            db.SaveChanges();
            Object[] data = new Object[4];
            data[0] = currentUser.MealPlan.CaloriesAdded;
            data[1] = currentUser.MealPlan.ProteinAdded;
            data[2] = currentUser.MealPlan.FatAdded;
            data[3] = currentUser.MealPlan.CarbsAdded;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult saveToMeal2(string name, string cals, string protein, string fat, string carbs, string serving)
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            MealPlan meal = db.MealDb.FirstOrDefault(x => x.Id == currentUser.Id);
            Meal2 meal2 = new Meal2
            {
                MealPlanId = meal.Id,
                FoodItem = Convert.ToString(name),
                servingSize = Convert.ToDouble(serving),
                calories = Convert.ToDouble(cals),
                protein = Convert.ToDouble(protein),
                fat = Convert.ToDouble(fat),
                carbs = Convert.ToDouble(carbs)
            };
            var currentCals = meal.CaloriesAdded;
            double calsAdded = Convert.ToDouble(cals);
            var currentCarbs = meal.CarbsAdded;
            double carbsAdded = Convert.ToDouble(carbs);
            var currentFat = meal.FatAdded;
            double FatAdded = Convert.ToDouble(fat);
            var currentProtein = meal.ProteinAdded;
            double ProteinAdded = Convert.ToDouble(protein);
            meal.FatAdded = Convert.ToDouble(currentFat) + FatAdded;
            meal.CarbsAdded = Convert.ToDouble(currentCarbs) + carbsAdded;
            meal.CaloriesAdded = Convert.ToDouble(currentCals) + calsAdded;
            meal.ProteinAdded = Convert.ToDouble(currentProtein) + ProteinAdded;
            db.Meal2Db.Add(meal2);
            db.SaveChanges();
            Object[] data = new Object[4];
            data[0] = currentUser.MealPlan.CaloriesAdded;
            data[1] = currentUser.MealPlan.ProteinAdded;
            data[2] = currentUser.MealPlan.FatAdded;
            data[3] = currentUser.MealPlan.CarbsAdded;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult saveToSnack2(string name, string cals, string protein, string fat, string carbs, string serving)
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            MealPlan meal = db.MealDb.FirstOrDefault(x => x.Id == currentUser.Id);
            Snack2 snack2 = new Snack2
            {
                MealPlanId = meal.Id,
                FoodItem = Convert.ToString(name),
                servingSize = Convert.ToDouble(serving),
                calories = Convert.ToDouble(cals),
                protein = Convert.ToDouble(protein),
                fat = Convert.ToDouble(fat),
                carbs = Convert.ToDouble(carbs)
            };
            var currentCals = meal.CaloriesAdded;
            double calsAdded = Convert.ToDouble(cals);
            var currentCarbs = meal.CarbsAdded;
            double carbsAdded = Convert.ToDouble(carbs);
            var currentFat = meal.FatAdded;
            double FatAdded = Convert.ToDouble(fat);
            var currentProtein = meal.ProteinAdded;
            double ProteinAdded = Convert.ToDouble(protein);
            meal.FatAdded = Convert.ToDouble(currentFat) + FatAdded;
            meal.CarbsAdded = Convert.ToDouble(currentCarbs) + carbsAdded;
            meal.CaloriesAdded = Convert.ToDouble(currentCals) + calsAdded;
            meal.ProteinAdded = Convert.ToDouble(currentProtein) + ProteinAdded;
            db.Snack2Db.Add(snack2);
            db.SaveChanges();
            Object[] data = new Object[4];
            data[0] = currentUser.MealPlan.CaloriesAdded;
            data[1] = currentUser.MealPlan.ProteinAdded;
            data[2] = currentUser.MealPlan.FatAdded;
            data[3] = currentUser.MealPlan.CarbsAdded;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult saveToMeal3(string name, string cals, string protein, string fat, string carbs, string serving)
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            MealPlan meal = db.MealDb.FirstOrDefault(x => x.Id == currentUser.Id);
            Meal3 meal3 = new Meal3
            {
                MealPlanId = meal.Id,
                FoodItem = Convert.ToString(name),
                servingSize = Convert.ToDouble(serving),
                calories = Convert.ToDouble(cals),
                protein = Convert.ToDouble(protein),
                fat = Convert.ToDouble(fat),
                carbs = Convert.ToDouble(carbs)
            };
            var currentCals = meal.CaloriesAdded;
            double calsAdded = Convert.ToDouble(cals);
            var currentCarbs = meal.CarbsAdded;
            double carbsAdded = Convert.ToDouble(carbs);
            var currentFat = meal.FatAdded;
            double FatAdded = Convert.ToDouble(fat);
            var currentProtein = meal.ProteinAdded;
            double ProteinAdded = Convert.ToDouble(protein);
            meal.FatAdded = Convert.ToDouble(currentFat) + FatAdded;
            meal.CarbsAdded = Convert.ToDouble(currentCarbs) + carbsAdded;
            meal.CaloriesAdded = Convert.ToDouble(currentCals) + calsAdded;
            meal.ProteinAdded = Convert.ToDouble(currentProtein) + ProteinAdded;
            db.Meal3Db.Add(meal3);
            db.SaveChanges();
            Object[] data = new Object[4];
            data[0] = currentUser.MealPlan.CaloriesAdded;
            data[1] = currentUser.MealPlan.ProteinAdded;
            data[2] = currentUser.MealPlan.FatAdded;
            data[3] = currentUser.MealPlan.CarbsAdded;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult getMeal1()
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            var Meal1Values = db.Meal1Db.Where(x => x.MealPlanId == currentUser.Id).Select(x => x).ToArray();
            return Json(Meal1Values, JsonRequestBehavior.AllowGet);
            
        }
        [HttpGet]
        public ActionResult getSnack1()
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            var Snack1Values = db.Snack1Db.Where(x => x.MealPlanId == currentUser.Id).Select(x => x).ToArray();
            return Json(Snack1Values, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult getMeal2()
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            var Meal2Values = db.Meal2Db.Where(x => x.MealPlanId == currentUser.Id).Select(x => x).ToArray();
            return Json(Meal2Values, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult getSnack2()
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            var snack2Values = db.Snack2Db.Where(x => x.MealPlanId == currentUser.Id).Select(x => x).ToArray();
            return Json(snack2Values, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult getMeal3()
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            var Meal3Values = db.Meal3Db.Where(x => x.MealPlanId == currentUser.Id).Select(x => x).ToArray();
            return Json(Meal3Values, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult deleteMeal1Item(string id)
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            int Id = Convert.ToInt16(id);
            Meal1 item = db.Meal1Db.Find(Id);
            MealPlan meal = db.MealDb.FirstOrDefault(x => x.Id == currentUser.Id);
            double calsLeft = Convert.ToDouble(meal.CaloriesAdded) - Convert.ToDouble(item.calories);
            meal.CaloriesAdded = calsLeft;
            double proLeft = Convert.ToDouble(meal.ProteinAdded) - Convert.ToDouble(item.protein);
            meal.ProteinAdded = proLeft;
            double carbsLeft = Convert.ToDouble(meal.CarbsAdded) - Convert.ToDouble(item.carbs);
            meal.CarbsAdded = carbsLeft;
            double fatLeft = Convert.ToDouble(meal.FatAdded) - Convert.ToDouble(item.fat);
            meal.FatAdded = fatLeft;
            db.Meal1Db.Remove(item);
            db.SaveChanges();
            var newDb = db.Meal1Db.Where(x => x.MealPlanId == meal.Id).Select(x => x).ToList();
            return Json(newDb, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult deleteMeal2Item(string id)
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            int Id = Convert.ToInt16(id);
            Meal2 item = db.Meal2Db.Find(Id);
            MealPlan meal = db.MealDb.FirstOrDefault(x => x.Id == currentUser.Id);
            double calsLeft = Convert.ToDouble(meal.CaloriesAdded) - Convert.ToDouble(item.calories);
            meal.CaloriesAdded = calsLeft;
            double proLeft = Convert.ToDouble(meal.ProteinAdded) - Convert.ToDouble(item.protein);
            meal.ProteinAdded = proLeft;
            double carbsLeft = Convert.ToDouble(meal.CarbsAdded) - Convert.ToDouble(item.carbs);
            meal.CarbsAdded = carbsLeft;
            double fatLeft = Convert.ToDouble(meal.FatAdded) - Convert.ToDouble(item.fat);
            meal.FatAdded = fatLeft;
            db.Meal2Db.Remove(item);
            db.SaveChanges();
            var newDb = db.Meal2Db.Where(x => x.MealPlanId == meal.Id).Select(x => x).ToList();
            return Json(newDb, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult deleteSnack1Item(string id)
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            int Id = Convert.ToInt16(id);
            Snack1 item = db.Snack1Db.Find(Id);
            MealPlan meal = db.MealDb.FirstOrDefault(x => x.Id == currentUser.Id);
            double calsLeft = Convert.ToDouble(meal.CaloriesAdded) - Convert.ToDouble(item.calories);
            meal.CaloriesAdded = calsLeft;
            double proLeft = Convert.ToDouble(meal.ProteinAdded) - Convert.ToDouble(item.protein);
            meal.ProteinAdded = proLeft;
            double carbsLeft = Convert.ToDouble(meal.CarbsAdded) - Convert.ToDouble(item.carbs);
            meal.CarbsAdded = carbsLeft;
            double fatLeft = Convert.ToDouble(meal.FatAdded) - Convert.ToDouble(item.fat);
            meal.FatAdded = fatLeft;
            db.Snack1Db.Remove(item);
            db.SaveChanges();
            var newDb = db.Snack1Db.Where(x => x.MealPlanId == meal.Id).Select(x => x).ToList();
            return Json(newDb, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult deleteSnack2Item(string id)
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            int Id = Convert.ToInt16(id);
            Snack2 item = db.Snack2Db.Find(Id);
            MealPlan meal = db.MealDb.FirstOrDefault(x => x.Id == currentUser.Id);
            double calsLeft = Convert.ToDouble(meal.CaloriesAdded) - Convert.ToDouble(item.calories);
            meal.CaloriesAdded = calsLeft;
            double proLeft = Convert.ToDouble(meal.ProteinAdded) - Convert.ToDouble(item.protein);
            meal.ProteinAdded = proLeft;
            double carbsLeft = Convert.ToDouble(meal.CarbsAdded) - Convert.ToDouble(item.carbs);
            meal.CarbsAdded = carbsLeft;
            double fatLeft = Convert.ToDouble(meal.FatAdded) - Convert.ToDouble(item.fat);
            meal.FatAdded = fatLeft;
            db.Snack2Db.Remove(item);
            db.SaveChanges();
            var newDb = db.Snack2Db.Where(x => x.MealPlanId == meal.Id).Select(x => x).ToList();
            return Json(newDb, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult deleteMeal3Item(string id)
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            int Id = Convert.ToInt16(id);
            Meal3 item = db.Meal3Db.Find(Id);
            MealPlan meal = db.MealDb.FirstOrDefault(x => x.Id == currentUser.Id);
            double calsLeft = Convert.ToDouble(meal.CaloriesAdded) - Convert.ToDouble(item.calories);
            meal.CaloriesAdded = calsLeft;
            double proLeft = Convert.ToDouble(meal.ProteinAdded) - Convert.ToDouble(item.protein);
            meal.ProteinAdded = proLeft;
            double carbsLeft = Convert.ToDouble(meal.CarbsAdded) - Convert.ToDouble(item.carbs);
            meal.CarbsAdded = carbsLeft;
            double fatLeft = Convert.ToDouble(meal.FatAdded) - Convert.ToDouble(item.fat);
            meal.FatAdded = fatLeft;
            db.Meal3Db.Remove(item);
            db.SaveChanges();
            var newDb = db.Meal3Db.Where(x => x.MealPlanId == meal.Id).Select(x => x).ToList();
            return Json(newDb, JsonRequestBehavior.AllowGet);
        }
    }
}
