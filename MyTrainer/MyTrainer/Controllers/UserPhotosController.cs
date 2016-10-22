using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyTrainer.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace MyTrainer.Controllers
{
    public class UserPhotosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: UserPhotos
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            var photoDb = db.PhotoDb.Where(x => x.UserId == currentUser.Id).Select(x => x).ToList();
            return View(photoDb);
        }

        // GET: UserPhotos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPhotos userPhotos = db.PhotoDb.Find(id);
            if (userPhotos == null)
            {
                return HttpNotFound();
            }
            return View(userPhotos);
        }


        // GET: UserPhotos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPhotos userPhotos = db.PhotoDb.Find(id);
            if (userPhotos == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.UserDb, "Id", "Username", userPhotos.UserId);
            return View(userPhotos);
        }

        // POST: UserPhotos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PictureDescription,DateTaken,Picture,FileName,UserId")] UserPhotos userPhotos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userPhotos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.UserDb, "Id", "Username", userPhotos.UserId);
            return View(userPhotos);
        }

        // GET: UserPhotos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPhotos userPhotos = db.PhotoDb.Find(id);
            if (userPhotos == null)
            {
                return HttpNotFound();
            }
            return View(userPhotos);
        }

        // POST: UserPhotos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserPhotos userPhotos = db.PhotoDb.Find(id);
            db.PhotoDb.Remove(userPhotos);
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
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            string userId = User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);

            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Images/"), pic);
                // file is uploaded
                file.SaveAs(path);
                UserPhotos photo = new UserPhotos
                {
                    FileName = "/Images/" + pic,
                    Picture = path.ToString(),
                    UserId = currentUser.Id
                };
                db.PhotoDb.Add(photo);
                db.SaveChanges();
                

            }
            // after successfully uploading redirect the user
            return RedirectToAction("Index");
        }


    }
}
