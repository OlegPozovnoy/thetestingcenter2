using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment1.Models;

namespace Assignment1.Controllers
{
    public class TestsController : Controller
    {
        //private DbModel db = new DbModel();

        IMockTests db;

        // GET: Tests

        public TestsController()
        {
            this.db = new IDataTests();
        }

        public TestsController(IMockTests mockDb)
        {
            this.db = mockDb;
        }

        public ActionResult Index()
        {
            return View("Index",db.Tests.OrderBy(c => c.Id).ToList());
        }

        // GET: Tests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            //Test test = db.Tests.Find(id);
            Test test = db.Tests.SingleOrDefault( c => c.Id == id);
            if (test == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
            return View("Details",test);
        }

        // GET: Tests/Create
        [Authorize]
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Tests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,author,name,description")] Test test)
        {
            if (ModelState.IsValid)
            {
                //db.Tests.Add(test);
                //db.SaveChanges();
                db.Save(test);
                return RedirectToAction("Index");
            }

            return View("Create", test);
        }

        // GET: Tests/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            //Test test = db.Tests.Find(id);
            Test test = db.Tests.SingleOrDefault(c => c.Id == id);
            if (test == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
            return View("Edit",test);
        }

        // POST: Tests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,author,name,description")] Test test)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(test).State = EntityState.Modified;
                //db.SaveChanges();
                db.Save(test);
                return RedirectToAction("Index");
            }
            return View("Edit",test);
        }

        // GET: Tests/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            //Test test = db.Tests.Find(id);
            Test test = db.Tests.SingleOrDefault(c => c.Id == id);
            if (test == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
            return View("Delete", test);
        }

        // POST: Tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            //Test test = db.Tests.Find(id);

            if (id == null)
            {
                return View("Error");
            }

            Test test = db.Tests.SingleOrDefault(c => c.Id == id);

            if (test == null)
            {
                return View("Error");
            }
            //db.Tests.Remove(test);
            //db.SaveChanges();
            db.Delete(test);

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
