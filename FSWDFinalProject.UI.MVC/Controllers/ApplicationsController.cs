﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FSWDFinalProject.DATA.EF;
using Microsoft.AspNet.Identity;

namespace FSWDFinalProject.UI.MVC.Controllers
{
    public class ApplicationsController : Controller
    {
        private JobBoardEntities db = new JobBoardEntities();

        // GET: Applications
        public ActionResult Index()
        {
            //get the current logged in users
            string currentUserID = User.Identity.GetUserId();

            //query for all Applications
            var applications = db.Applications.Include(a => a.ApplicationStatus).Include(a => a.OpenPosition).Include(a => a.UserDetail);

            //admins see all applications
            if (User.IsInRole("Admin"))
            {
                //no filtering - All applications Presented
                return View(applications);
            }
            //-------------------------------------------------------

            //Managers see applications for their location
            //check their  role
            if (User.IsInRole("Manager"))
            {
                var managersApplications = applications.Where(l => l.OpenPosition.Location.ManagerId == currentUserID);

                return View(managersApplications);
            }

            //get the location associated to that manager

            //add in the where clause to filter - (using the location variable above)
            //var managersApplications = applications.where...

            //return view(managerApplications)
            //-------------------------------------------------------

            //employees see applications they have submitted

            if (User.IsInRole("Employee"))
            {
                var employeesApplications = applications.Where(l => l.UserDetail.UserId == currentUserID);

                return View(employeesApplications);
            }
            //var employeeApplications = applications.where...

            //return the view(employeeApplications)

            return View(applications.ToList());
        }

        // GET: Applications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // GET: Applications/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.ApplicationStatusId = new SelectList(db.ApplicationStatuses, "ApplicationStatusId", "StatusName");
            ViewBag.OpenPositionId = new SelectList(db.OpenPositions, "OpenPositionId", "OpenPositionId");
            ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "FirstName");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicationId,OpenPositionId,UserId,ApplicationDate,ManagerNotes,ApplicationStatusId,ResumeFilename")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Applications.Add(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationStatusId = new SelectList(db.ApplicationStatuses, "ApplicationStatusId", "StatusName", application.ApplicationStatusId);
            ViewBag.OpenPositionId = new SelectList(db.OpenPositions, "OpenPositionId", "OpenPositionId", application.OpenPositionId);
            ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "FirstName", application.UserId);
            return View(application);
        }

        // GET: Applications/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationStatusId = new SelectList(db.ApplicationStatuses, "ApplicationStatusId", "StatusName", application.ApplicationStatusId);
            ViewBag.OpenPositionId = new SelectList(db.OpenPositions, "OpenPositionId", "OpenPositionId", application.OpenPositionId);
            ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "FirstName", application.UserId);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplicationId,OpenPositionId,UserId,ApplicationDate,ManagerNotes,ApplicationStatusId,ResumeFilename")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationStatusId = new SelectList(db.ApplicationStatuses, "ApplicationStatusId", "StatusName", application.ApplicationStatusId);
            ViewBag.OpenPositionId = new SelectList(db.OpenPositions, "OpenPositionId", "OpenPositionId", application.OpenPositionId);
            ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "FirstName", application.UserId);
            return View(application);
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin, Manager")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.Applications.Find(id);
            db.Applications.Remove(application);
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
