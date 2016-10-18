﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyTrainer.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<User> UserDb { get; set; }
        public DbSet<Workouts> WorkoutDb { get; set; }
        public DbSet<Goals> GoalDb { get; set; }
        public DbSet<MealPlan> MealDb { get; set; }
        public DbSet<UserPhotos> PhotoDb { get; set; }
        public DbSet<UserSchedule> ScheduleDb { get; set; }
        public DbSet<VegetarianMealPlan> VegetarianDb { get; set; }
        public DbSet<VeganMealPlan> VeganDb { get; set; }
        public DbSet<BasicMealPlan> BasicDb { get; set; }
    }
}