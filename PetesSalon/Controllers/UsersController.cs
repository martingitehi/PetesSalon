using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetesSalon.DomainClasses;

namespace PetesSalon.Controllers
{
    public class UsersController : Controller
    {
        private Salon db = new Salon();

        // GET: Users
        public async Task<ActionResult> Index()
        {
            var user = Session["User"] as User;

            if (user != null && user.Role == "Admin")
            {
                return View(await db.User.ToListAsync());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
        }

        public async Task<ActionResult> List(string category)
        {
            var user = Session["User"] as User;
            if (user != null && user.Role == "Admin")
            {
                return View(await db.User.Where(x => x.Role == category).ToListAsync());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.User.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserId,UserName,Password,Email,Role,FullName")] User user)
        {
            if (ModelState.IsValid)
            {
                user.UserId = Guid.NewGuid();
                db.User.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.User.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserId,UserName,Password,Email,Role,FullName")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.User.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        //GET: TODO: Load the Login View
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string userName, string passcode)
        {
            User user = await db.User.Where(u => u.UserName == userName).FirstOrDefaultAsync();

            if (user == null)
            {
                return HttpNotFound();
            }

            else if (user != null)
            {
                if (user.UserName.Equals(userName) && user.Password.Equals(passcode))
                {
                    Session["User"] = new User
                    {
                        UserId = user.UserId,
                        FullName = user.FullName.ToString(),
                        Role = user.Role.ToString()
                    };

                    ViewBag.User = Session["User"];
                    if (user.Role == "Admin")
                    {
                        return RedirectToAction("Index", "Users");
                    }
                    else if (user.Role == "Employee")
                    {
                        return RedirectToAction("Index", "ProductAndServices");
                    }
                }
                else
                {
                    ViewBag.Error = "The username and/or password entered is wrong.";
                }
            }
            Session["User"] = null;
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            User user = await db.User.FindAsync(id);
            db.User.Remove(user);
            await db.SaveChangesAsync();
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
