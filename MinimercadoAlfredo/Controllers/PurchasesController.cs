﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MinimercadoAlfredo.Context;
using MinimercadoAlfredo.Models;
using MinimercadoAlfredo.ViewModels;

namespace MinimercadoAlfredo.Controllers
{
    public class PurchasesController : Controller
    {
        private AlfredoContext db = new AlfredoContext();

        // GET: Purchases
        public ActionResult Index()
        {
            var purchases = db.Purchases.Include(p => p.Provider);
            return View(purchases.ToList().OrderByDescending(p => p.PurchaseDate));
        }

        // GET: Purchases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // GET: Purchases/Create
        public ActionResult Create()
        {
            ViewBag.Providers = db.Providers.ToList().OrderBy(p => p.ProviderName);
            ViewBag.Products = db.Products.ToList().OrderBy(p => p.ProductDescription);
            return View("CreatePurchase");
        }

        // POST: Purchases/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Create(PurchaseVM purchase)
        {
            bool status = false;
            Purchase p = new Purchase();
            var idprovider = Int32.Parse(purchase.ProviderName);

            if (ModelState.IsValid)
            {
                p.IdProvider = idprovider;
                p.PurchaseDate = purchase.PurchaseDate;
                p.Comments = purchase.PurchaseComments;
                p.PurchaseTotal = purchase.PurchaseTotal;

                db.Purchases.Add(p);
                db.SaveChanges();

                foreach (var item in purchase.PurchaseLines)
                {
                    PurchaseLine purchaseline = new PurchaseLine();
                    purchaseline.IdPurchase = p.IdPurchase;
                    purchaseline.IdProduct = item.IdProduct;
                    purchaseline.Cost = item.Cost;
                    purchaseline.PurchaseQuantity = item.PurchaseQuantity;
                    purchaseline.LineTotal = item.LineTotal;

                    db.PurchaseLines.Add(purchaseline);
                    db.SaveChanges();
                }
                status = true;
            }

            return new JsonResult { Data = new { status = status } };
        }

        // GET: Purchases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProvider = new SelectList(db.Providers, "IdProvider", "ProviderName", purchase.IdProvider);
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPurchase,PurchaseDate,Comments,PurchaseTotal,IdProvider")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProvider = new SelectList(db.Providers, "IdProvider", "ProviderName", purchase.IdProvider);
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            db.Purchases.Remove(purchase);
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
