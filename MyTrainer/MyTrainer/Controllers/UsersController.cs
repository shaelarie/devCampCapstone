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
        public ActionResult Index([Bind(Include = "Id,Weight,HeightFt,HeightIn,LoginId,TDEE,DailyCalorieIntake,Gender")] User user)
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
            Goals userGoal = db.GoalDb.FirstOrDefault(x => x.Id == currentUser.Id);

            //if(currentUser.Goals.UserGoal == userGoal.UserGoal && userGoal.UserGoal == "GainMuscle")
            //{
            //    currentUser.TDEE = currentUser.
            //}

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
        [Authorize]
        public ActionResult Edit()
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            int id = currentUser.Id;
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
        public ActionResult Edit([Bind(Include = "Id,Username,Weight,HeightFt,HeightIn,TDEE,DailyCalorieIntake,Gender,LoginId," +
            "GoalId,MealPlanId,BMI,ProteinIntake,FatIntake,CarbIntake,BMR,WorkoutAmount,age")] User user)
        {
            MealPlan mealPlan = db.MealDb.FirstOrDefault(x => x.Id == user.Id);
            Goals goal = db.GoalDb.FirstOrDefault(x => x.Id == user.Id);
            if (ModelState.IsValid)
            {
                var userGoal = goal.UserGoal.ToString();
                if (user.Gender.ToString() == "Male")
                {
                    user.BMR = getMaleBMR(Convert.ToInt32(user.Weight),Convert.ToInt16(user.HeightFt),Convert.ToInt16(user.HeightIn),Convert.ToInt16(user.age));
                    user.BMI = getBMI(Convert.ToInt32(user.Weight), Convert.ToInt32(user.HeightFt), Convert.ToInt32(user.HeightIn));
                    user.TDEE = getTDEE(Convert.ToDouble(user.BMR), Convert.ToInt32(user.WorkoutAmount));
                }
                else if (user.Gender.ToString() == "Female")
                {
                    user.BMR = getFemaleBMR(Convert.ToInt32(user.Weight), Convert.ToInt16(user.HeightFt), Convert.ToInt16(user.HeightIn), Convert.ToInt16(user.age));
                    user.BMI = getBMI(Convert.ToInt32(user.Weight), Convert.ToInt32(user.HeightFt), Convert.ToInt32(user.HeightIn));
                    user.TDEE = getTDEE(Convert.ToDouble(user.BMR), Convert.ToInt32(user.WorkoutAmount));
                }
                user.DailyCalorieIntake = getCalories(Convert.ToInt32(user.TDEE), userGoal);
                user.ProteinIntake = getProtein(Convert.ToDouble(user.BMI), Convert.ToInt32(user.Weight));
                user.FatIntake = getFat(Convert.ToInt16(user.DailyCalorieIntake));
                user.CarbIntake = getCarbs(Convert.ToDouble(user.ProteinIntake), Convert.ToDouble(user.FatIntake), Convert.ToDouble(user.DailyCalorieIntake));

                var schedule = db.ScheduleDb.Where(x => x.UserId == user.Id).Select(x => x);
                Math.Round(Convert.ToDouble(user.BMR));
                Math.Round(Convert.ToDouble(user.BMI));
                mealPlan.CalorieIntake = user.DailyCalorieIntake;
                mealPlan.CarbIntake = user.CarbIntake;
                mealPlan.ProteinIntake = user.ProteinIntake;
                mealPlan.FatIntake = user.FatIntake;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "MealPlans");
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

        public double getMaleBMR(int weight, int heightFt,int heightIn, int age)
        {
            int ftToIn = heightFt * 12;
            int heightInInches = ftToIn + heightIn;
            double maleBMR = 66 + (6.23 * weight) + (12.7 * heightInInches) - (6.8 * age);
            return maleBMR;
        }

        public double getFemaleBMR(int weight, int heightFt, int heightIn, int age)
        {   int ftToIn = heightFt * 12;
            int heightInInches = ftToIn + heightIn;
            double FemaleBMR = 655 + (4.35 * weight) + (4.7 * heightInInches) - (4.7 * age);
            return FemaleBMR;
        }

        public double getBMI(int weight, int heightFt, int heightIn)
        {
            double weightKg = weight * .45;
            double ftHeightInInches = heightFt * 12;
            double HeightInInches = ftHeightInInches + heightIn;
            double HeightInMeters = (HeightInInches * .025);
            double UserBMI = weightKg / (HeightInMeters * HeightInMeters);
            return Convert.ToDouble(UserBMI);
        }

        public int getTDEE(double UserBMR, int daysWorkingOut)
        {
            double userTDEE;
            if(daysWorkingOut <= 0)
            {
                userTDEE = UserBMR * 1.2;
                return Convert.ToInt32(userTDEE);
            }
            else if (daysWorkingOut >= 1 && daysWorkingOut <= 3)
            {
                userTDEE = UserBMR * 1.3;
                return Convert.ToInt32(userTDEE);

            }
            else if (daysWorkingOut >= 4 && daysWorkingOut <= 5)
            {
                userTDEE = UserBMR * 1.5;
                return Convert.ToInt32(userTDEE);

            }
            else

            userTDEE = UserBMR * 1.7;
            return Convert.ToInt32(userTDEE);

        }

        public int getProtein(double bmi, int weight)
        {
            double protein;
            if(bmi >= 30)
            {
                protein = weight;
                return Convert.ToInt16(protein);
            }
            else
                protein = weight+(weight * .1);
                return Convert.ToInt16(protein);

        }
        public double getCalories(int tdee, string goal)
        {
            double calories;
            if (goal == "GainMuscle")
            {
                calories = tdee + (tdee * .2);
                return Convert.ToInt16(calories);
            }
            else if (goal == "LoseWeight")
            {
                calories = tdee - (tdee * .17);
                return Convert.ToInt16(calories);
            }
            else
                calories = tdee;
            return Convert.ToInt16(calories);

        }
        public double getFat(double dailyIntake)
        {
            double fat = (dailyIntake * .38) / 9;
            return Convert.ToInt16(fat);

        }
        public double getCarbs(double protein, double fat, double dailyIntake)
        {
            double carbs = (dailyIntake - (protein * 4) - (fat * 9)) / 4;
            return Convert.ToInt16(carbs);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult getMaxMacros()
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            MealPlan meal = db.MealDb.FirstOrDefault(x => x.Id == currentUser.Id);
            Object[] data = new Object[12];
            data[0] = currentUser.DailyCalorieIntake;
            data[1] = currentUser.ProteinIntake;
            data[2] = currentUser.FatIntake;
            data[3] = currentUser.CarbIntake;
            data[4] = Convert.ToInt16((meal.CaloriesAdded / currentUser.DailyCalorieIntake) * 100);
            data[5] = Convert.ToInt16((meal.ProteinAdded / currentUser.ProteinIntake) * 100);
            data[6] = Convert.ToInt16((meal.FatAdded / currentUser.FatIntake) * 100);
            data[7] = Convert.ToInt16((meal.CarbsAdded / currentUser.CarbIntake) * 100);
            data[8] = Convert.ToInt16(meal.CaloriesAdded);
            data[9] = Convert.ToInt16(meal.ProteinAdded);
            data[10] = Convert.ToInt16(meal.FatAdded);
            data[11] = Convert.ToInt16(meal.CarbsAdded);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public string SerializeMacros(Object List)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(List);
            return json;

        }

        [HttpGet]
        public ActionResult getMaxFat()
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            var dailyFat = currentUser.FatIntake.Value;
            return Json(dailyFat, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult getMaxProtein()
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            var dailyProtein = currentUser.ProteinIntake.Value;
            return Json(dailyProtein, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult getMaxCarbs()
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            var dailyCarbs = currentUser.CarbIntake.Value;
            return Json(dailyCarbs, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        public ActionResult ChatRoom()
        {
            return View();
        }

        [HttpPost]
        public ActionResult saveMessages(string text)
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            Chatroom chat = new Chatroom
            {
                userId = currentUser.Id,
                name = currentUser.Username.ToString(),
                messages = text.ToString()
            };
            db.ChatDb.Add(chat);
            db.SaveChanges();
            return Json(chat, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult getMessages()
        {
            var messages = db.ChatDb.ToList();
            return Json(messages, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult getPictures()
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            var userPhotos = db.PhotoDb.Where(x => x.UserId == currentUser.Id).Select(x => x.FileName).ToArray();            
            return Json(userPhotos, JsonRequestBehavior.AllowGet);
        }
    }
}
