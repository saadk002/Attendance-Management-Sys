using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ams.Models;

namespace Ams.Controllers
{
    public class ClassesController : Controller
    {
        private FINALEntities db = new FINALEntities();

        // GET: Classes
        public ActionResult Index()
        {
            var classes = db.Classes.Include(x => x.Cours).Include(x => x.Teacher);
            return View(classes.ToList());
        }

        // GET: Classes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // GET: Classes/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "FirstName");
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Class @class)
        {
            if (ModelState.IsValid)
            {
                List<Teacher> teachers = db.Teachers.ToList();
                Teacher teacher = teachers.FirstOrDefault(x => x.TeacherID == @class.TeacherID);
                string teacherId = teacher.FirstName;

                List<Cours> courses = db.Courses.ToList();
                Cours course = courses.FirstOrDefault(x => x.CourseID == @class.CourseID);
                string courseId = course.CourseName;
                string courseCode = course.CourseCode;

                @class.Schedule = teacherId +" - "+courseId + " - " +courseCode;
                db.Classes.Add(@class);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", @class.CourseID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "FirstName", @class.TeacherID);
            return View(@class);
        }

        // GET: Classes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", @class.CourseID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "FirstName", @class.TeacherID);
            return View(@class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassID,CourseID,TeacherID,Schedule")] Class @class)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@class).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", @class.CourseID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "FirstName", @class.TeacherID);
            return View(@class);
        }

        // GET: Classes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Class @class = db.Classes.Find(id);
            db.Classes.Remove(@class);
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
