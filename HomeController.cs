using DBFirstApproach.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace DBFirstApproach.Controllers
{
    public class HomeController : Controller
    {
        private CompanyDBEntities _dbContext;
        public HomeController()
        {
            _dbContext = new CompanyDBEntities();
        }
        // GET: Home
        public ActionResult Index()
        {
            List<EMP> Emps = _dbContext.EMPs.ToList();
            return View(Emps);
        }

        //Get Single record
        public ActionResult Details( int id)
        {
            EMP emp = _dbContext.EMPs.Find(id);
                return View(emp);
        }

        //Insert the data
       
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(EMP obj)
        {
            _dbContext.EMPs.Add(obj);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        //Update the data
        public ActionResult Edit(int id)
        {
            EMP emp = _dbContext.EMPs.Find(id);
            return View(emp);
        }
        [HttpPost]
        public ActionResult Edit(EMP obj)
        {
           
            _dbContext.Entry(obj).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        //Delete the Record
        [HttpGet]
        public ActionResult Delete(int id)
        {
            EMP emp = _dbContext.EMPs.Find(id);
            return View(emp);

        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            int n = Convert.ToInt32(id);
            EMP emp = _dbContext.EMPs.Find(n);
            _dbContext.EMPs.Remove(emp);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}