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
    public class OrdersController : Controller
    {
        private Context db = new Context();

        // GET: Orders
        public ActionResult Index()
        {
            ViewBag.Message = "ORDERS HISTORY";
            var orders = db.orders.Include(o => o.cust).Include(o => o.menu).Include(o => o.vend);
            return View(orders.ToList());
        }

        public ActionResult AllOrders()
        {
            var orders = db.orders.Include(o => o.cust).Include(o => o.menu).Include(o => o.vend);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.CustId = new SelectList(db.customers, "Id", "Name");
            ViewBag.MenuId = new SelectList(db.menus, "FoodId", "FoodName","FoodPrice");
            ViewBag.VendId = new SelectList(db.vendors, "Id", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrdId,Quantity,TotalBill,Status,MenuId,CustId,VendId")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                Menu m = db.menus.FirstOrDefault(model => model.FoodId == orders.MenuId);
                orders.TotalBill = orders.Quantity * m.FoodPrice;
                orders.Status = "PENDING";
                db.orders.Add(orders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustId = new SelectList(db.customers, "Id", "Name", orders.CustId);
            ViewBag.MenuId = new SelectList(db.menus, "FoodId", "FoodName", orders.MenuId);
            ViewBag.VendId = new SelectList(db.vendors, "Id", "Name", orders.VendId);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustId = new SelectList(db.customers, "Id", "Name", orders.CustId);
            ViewBag.MenuId = new SelectList(db.menus, "FoodId", "FoodName", orders.MenuId);
            ViewBag.VendId = new SelectList(db.vendors, "Id", "Name", orders.VendId);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrdId,Quantity,TotalBill,Status,MenuId,CustId,VendId")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AllOrders");
            }
            ViewBag.CustId = new SelectList(db.customers, "Id", "Name", orders.CustId);
            ViewBag.MenuId = new SelectList(db.menus, "FoodId", "FoodName", orders.MenuId);
            ViewBag.VendId = new SelectList(db.vendors, "Id", "Name", orders.VendId);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orders orders = db.orders.Find(id);
            db.orders.Remove(orders);
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
