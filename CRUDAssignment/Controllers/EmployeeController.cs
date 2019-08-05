using CRUDAssignment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDAssignment.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewAll()
        {
            return View(GetAllEmployees());
        }
        IEnumerable<Employee> GetAllEmployees()
        {
            using (CRUDAssignmentEntities emp = new CRUDAssignmentEntities())
            {
                return emp.Employees.ToList<Employee>();
            }
        }
        public ActionResult AddorEdit(int id=0)
        {
            Employee emp = new Employee();
            if (id != 0)
            { 
                using(CRUDAssignmentEntities db = new CRUDAssignmentEntities())
                {
                    emp = db.Employees.Where(x => x.EmployeeID == id).FirstOrDefault<Employee>();
                }
            }
            return View(emp);
        }
        [HttpPost]
        public ActionResult AddorEdit(Employee emp)
        {
            try
            {
                using (CRUDAssignmentEntities db = new CRUDAssignmentEntities())
                {
                    if (emp.EmployeeID == 0)
                    {
                        db.Employees.Add(emp);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    
                }

                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllEmployees()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (CRUDAssignmentEntities db = new CRUDAssignmentEntities())
                {
                    Employee emp = db.Employees.Where(x => x.EmployeeID == id).FirstOrDefault<Employee>();
                    db.Employees.Remove(emp);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllEmployees()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        
    }
}