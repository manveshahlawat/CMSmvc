using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyCMS.Models;

namespace MyCMS.Controllers
{
    public class CustomersController : Controller
    {
        private Context db = new Context();

        // GET: Customers
        public ActionResult Index()
        {
            ViewBag.Message = "Customers";
            return View(db.customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Message = "WELCOME";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,PhoneNumber,Email,ConfirmEmail,DOB,Password,ConfirmPassword")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("CustLogin");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,PhoneNumber,Email,ConfirmEmail,DOB,Password,ConfirmPassword")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CustomerDashboard",new { customer.Id});
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.customers.Find(id);
            db.customers.Remove(customer);
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

        public ActionResult CustLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CustLogin(Customer cust)
        {
            using (Context con = new Context())
            {
                var CustDetails = con.customers.Where(x => x.Email == cust.Email && x.Password == cust.Password).FirstOrDefault();
                if (CustDetails == null)
                {
                    return HttpNotFound();

                }
                else
                {
                    Session["Id"] = CustDetails.Id;
                    return RedirectToAction("CustomerDashboard", "Customers");
                }
                //return View();
            }
        }

        public ActionResult CustomerDashboard()
        {
            Customer c = new Customer();
            c.Id = Convert.ToInt32(Session["Id"].ToString());
            if (Session["Id"] != null)
            {
                return View(c);
            }
            else
            {
                return RedirectToAction("CustLogin");
            }

        }
    }
}
