using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetesSalon.DomainClasses;
using System.Threading.Tasks;

namespace PetesSalon.Controllers
{
    public class ProductAndServicesController : Controller
    {
        private Salon db = new Salon();

        // GET: ProductAndServices
        public async Task<ActionResult> Index(string category)
        {
            if (category != null)
            {
                return View(await db.Products.Where(x => x.ProductName == category).ToListAsync());
            }

            return View(db.Products.ToList());
            
        }

        // GET: ProductAndServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductAndService productAndService = db.Products.Find(id);
            if (productAndService == null)
            {
                return HttpNotFound();
            }
            return View(productAndService);
        }

        // GET: ProductAndServices/Create
        public ActionResult Create()
        {
            return View();
        }
               
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(
            Include = "ProductId,ProductName,ProductPricing,Image,Description,Type")]
        ProductAndService productAndService, HttpPostedFileBase Photo)
        {
            ProductAndService duplicate = await db.Products
                .Where(p => p.ProductName == productAndService.ProductName)
                .FirstOrDefaultAsync();

            if (duplicate == null) {

                byte[] picData = new byte[Photo.ContentLength];
                Photo.InputStream.Read(picData, 0, Photo.ContentLength);
                productAndService.Image = picData;

                db.Products.Add(productAndService);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(productAndService);
            }
        }

        // GET: ProductAndServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductAndService productAndService = db.Products.Find(id);

            if (productAndService == null)
            {
                return HttpNotFound();
            }
            return View(productAndService);
        }

        public async Task<ActionResult> Search(string searchString)
        {
            if (!(string.IsNullOrWhiteSpace(searchString)))
            {
                return View("Index", await db.Products.Where(p => p.ProductName.Contains(searchString)).ToListAsync());
            }
            return View("Index", await db.Products.ToListAsync());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,ProductPricing,Image,Description,Type")]
        ProductAndService productAndService, HttpPostedFileBase Photo)
        {
            if (!(Photo.ContentLength == 0) && Photo != null)
            {
                byte[] picData = new byte[Photo.ContentLength];
                Photo.InputStream.Read(picData, 0, Photo.ContentLength);
                productAndService.Image = picData;

                db.Entry(productAndService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index"); 
            }
            else
                return View(productAndService);
        }

        // GET: ProductAndServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductAndService productAndService = db.Products.Find(id);
            if (productAndService == null)
            {
                return HttpNotFound();
            }
            return View(productAndService);
        }

        // POST: ProductAndServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductAndService productAndService = db.Products.Find(id);
            db.Products.Remove(productAndService);
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
