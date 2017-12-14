using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HelloWorld.Models;
namespace HelloWorld.ViewModel
{
    public class CustomerViewModel
    {
        // Customer
        public Customer customer { get; set; }
        // List of customers
        public List<Customer> customers { get; set; }
    }
}