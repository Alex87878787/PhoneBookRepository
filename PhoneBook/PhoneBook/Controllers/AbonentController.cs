using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhoneBook.Models;
using PhoneBook.DAL;
using PhoneBook.ViewModels;

namespace PhoneBook.Controllers
{
    public class AbonentController : Controller
    {
        private PhoneBookDb db = new PhoneBookDb();

        //
        // GET: /Abonent/

        //public ActionResult Index()
        //{
        //    var viewModel = new AbonentIndexData();
        //    viewModel.Abonents = db.Abonents.Include(i => i.Numbers);
        //    //var abonents = db.Abonents.Include(i => i.Numbers);
        //    return View(viewModel);
        //    //return View(db.Abonents.ToList());
        //}

        public ActionResult Index(int? id)
        {
            var viewModel = new AbonentIndexData();
            viewModel.Abonents = db.Abonents
                .Include(i => i.Numbers)
                .OrderBy(i => i.LastName);

            if (id != null)
            {
                ViewBag.AbonentId = id.Value;
                viewModel.Numbers = viewModel.Abonents.Where(
                    i => i.AbonentId == id.Value).Single().Numbers;
            }
            return View(viewModel);
        }
        //
        // GET: /Abonent/Details/5

        public ActionResult Details(int id = 0)
        {
            Abonent abonent = db.Abonents.Find(id);
            if (abonent == null)
            {
                return HttpNotFound();
            }
            return View(abonent);
        }

        //
        // GET: /Abonent/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Abonent/Create
        //тут был я
        [HttpPost]
        public ActionResult Create(Abonent abonent,Number number)
        {
            if (ModelState.IsValid)
            {
                db.Abonents.Add(abonent);
                db.SaveChanges();
                db.Numbers.Add(number);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(abonent);
        }

        //
        // GET: /Abonent/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Abonent abonent = db.Abonents.Find(id);
            if (abonent == null)
            {
                return HttpNotFound();
            }
            return View(abonent);
        }

        //
        // POST: /Abonent/Edit/5

        [HttpPost]
        public ActionResult Edit(Abonent abonent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(abonent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(abonent);
        }

        //
        // GET: /Abonent/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Abonent abonent = db.Abonents.Find(id);
            if (abonent == null)
            {
                return HttpNotFound();
            }
            return View(abonent);
        }

        //
        // POST: /Abonent/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Abonent abonent = db.Abonents.Find(id);
            db.Abonents.Remove(abonent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}