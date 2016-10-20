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
        //// GET: Users/Create
        //public ActionResult Create()
        //{

        //    ViewBag.LoginId = new SelectList(db.Users, "Id", "Email");
        //    return View();
        //}

        //// POST: Users/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Weight,HeightFt,HeightIn,LoginId,TDEE,DailyCalorieIntake")] User user)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        db.UserDb.Add(user);
        //        db.SaveChanges();
        //        return RedirectToAction("Index", user);
        //    }

        //    ViewBag.EmailId = new SelectList(db.Users, "Id", "Email", user.LoginId);
        //    return View(user);
        //}

        // GET: Users/Edit/5
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
            "GoalId,MealPlanId,BMI,ProteinIntake,FatIntake,CarbIntake,BMR,WorkoutAmount")] User user)
        {
            if (ModelState.IsValid)
            {
                var userGoal = db.GoalDb.Where(x => x.Id == user.Id).Select(x => x.UserGoal).ToString();
                if (user.Gender.ToString() == "Male")
                {
                    user.BMR = getMaleBMR(Convert.ToInt32(user.Weight));
                    user.BMI = getBMI(Convert.ToInt32(user.Weight), Convert.ToInt32(user.HeightFt), Convert.ToInt32(user.HeightIn));
                    user.TDEE = getTDEE(Convert.ToDouble(user.BMR), Convert.ToInt32(user.WorkoutAmount));
                }
                else if (user.Gender.ToString() == "Female")
                {
                    user.BMR = getFemaleBMR(Convert.ToInt32(user.Weight));
                    user.BMI = getBMI(Convert.ToInt32(user.Weight), Convert.ToInt32(user.HeightFt), Convert.ToInt32(user.HeightIn));
                    user.TDEE = getTDEE(Convert.ToDouble(user.BMR), Convert.ToInt32(user.WorkoutAmount));
                }
                user.DailyCalorieIntake = getCalories(Convert.ToDouble(user.TDEE), userGoal);
                user.ProteinIntake = getProtein(Convert.ToDouble(user.BMI), Convert.ToInt32(user.Weight));
                double caloriesAfterProtein = (Convert.ToInt16(user.DailyCalorieIntake)) - (Convert.ToInt16(user.ProteinIntake) * 4);
                user.FatIntake = getFat(caloriesAfterProtein);
                user.CarbIntake = getCarbs(Convert.ToDouble(user.ProteinIntake), Convert.ToDouble(user.FatIntake), Convert.ToDouble(user.DailyCalorieIntake));

                var schedule = db.ScheduleDb.Where(x => x.UserId == user.Id).Select(x => x);
                Math.Round(Convert.ToDouble(user.BMR));
                Math.Round(Convert.ToDouble(user.BMI));
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

        public double getMaleBMR(int weight)
        {
            double weightKg = weight * .04;
            double MaleBMR = (875 + 10.2) * weightKg;
            return MaleBMR;
        }

        public double getFemaleBMR(int weight)
        {
            double weightKg = weight * .04;
            double FemaleBMR = (795 + 7.2) * weightKg;
            return FemaleBMR;
        }

        public int getBMI(int weight, int heightFt, int heightIn)
        {
            double weightKg = weight * .04;
            double ftHeightInInches = heightFt * 12;
            double HeightInInches = ftHeightInInches + heightIn;
            double HeightInMeters = (ftHeightInInches * .025);
            double UserBMI = weightKg / (HeightInMeters * HeightInMeters);
            return Convert.ToInt16(UserBMI);
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
                protein = weight * .8;
                return Convert.ToInt16(protein);
            }
            else
                protein = weight;
                return Convert.ToInt16(protein);

        }
        public double getCalories(double tdee, string goal)
        {
            double calories;
            if (goal == "GainMuscle")
            {
                calories = tdee + (tdee * .2);
                return Convert.ToInt16(calories);
            }
            else if (goal == "LoseWeight")
            {
                calories = tdee - (tdee * .2);
                return Convert.ToInt16(calories);
            }
            else
                calories = tdee;
            return Convert.ToInt16(calories);

        }
        public double getFat(double dailyIntake)
        {
            double fat = (dailyIntake * .25) / 9;
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

    }
}
