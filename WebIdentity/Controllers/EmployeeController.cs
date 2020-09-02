using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebIdentity.Models;

namespace WebIdentity.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        
        private ApplicationUserManager _userManager;

        public EmployeeController()
        {

        }

        public EmployeeController(ApplicationUserManager userManager)
        {

            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Role
        public ActionResult Index()
        {
            List<Employee> list = new List<Employee>();
            foreach (var user in UserManager.Users)
                list.Add(new Employee(user));
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Employee model)
        {
            var usr = new ApplicationUser() { Email = model.Email, UserName = model.UserName };
            await UserManager.CreateAsync(usr);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(string id)
        {
            var usr = await UserManager.FindByIdAsync(id);
            return View(new Employee(usr));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Employee model)
        {
            //var usr = new ApplicationUser() { Id = model.Id, Email = model.Email, UserName = model.UserName };
            var usr = await UserManager.FindByIdAsync(model.Id);
            usr.UserName = model.UserName;
            await UserManager.UpdateAsync(usr);

            //await UserManager.UpdateAsync(usr);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Details(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            return View(new Employee(user));
        }
        public async Task<ActionResult> Delete(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            return View(new Employee(user));
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            await UserManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }
        
    }
}