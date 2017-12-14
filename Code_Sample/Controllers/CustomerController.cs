using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelloWorld.Models;
using HelloWorld.Dal;
using HelloWorld.ViewModel;
using System.Threading;
namespace HelloWorld.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Load() // connect via browser HTML
        {
            Customer obj = 
                new Customer 
                    { 
                        CustomerCode = "1001", 
                        CustomerName = "Shiv" 
                    };
            if (Request.QueryString["Type"] == "HTML")
            {
                return View("Customer", obj);
            }
            else
            {
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
       
        public ActionResult Enter()
        {
            // view model object
            CustomerViewModel obj = new CustomerViewModel();
            // // single object is fresh
            obj.customer = new Customer();
            // dal i have filled the customers collection
            return View("EnterCustomer", obj);
        }
        public ActionResult EnterSearch()
        {
            CustomerViewModel obj = new CustomerViewModel();
            obj.customers = new List<Customer>();
            return View("SearchCustomer", obj);
        }
        public ActionResult getCustomers() // JSON collection
        {
            CustomerDal dal = new CustomerDal();
            List<Customer> customerscoll = dal.Customers.ToList<Customer>();
            return Json(customerscoll, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SearchCustomer()
        {
            CustomerViewModel obj = new CustomerViewModel();
            CustomerDal dal = new CustomerDal();
            string str = Request.Form["txtCustomerName"].ToString();
            List<Customer> customerscoll
                = (from x in dal.Customers
                   where x.CustomerName == str
                   select x).ToList<Customer>();
            obj.customers = customerscoll;
            return View("SearchCustomer", obj);
        }
        public ActionResult Submit() // validation runs
        {
           
            Customer obj = new Customer();
            obj.CustomerName = Request.Form["Customer.CustomerName"];
            obj.CustomerCode = Request.Form["Customer.CustomerCode"];
            if(ModelState.IsValid)
            {
                // insert the customer object to database
                // EF DAL
                CustomerDal Dal = new CustomerDal();
                Dal.Customers.Add(obj); // in mmemory
                Dal.SaveChanges(); // physical commit
            }
            
            CustomerDal dal = new CustomerDal();
            List<Customer> customerscoll = dal.Customers.ToList<Customer>();
            
            return Json(customerscoll, JsonRequestBehavior.AllowGet);
            
        }
    }
}