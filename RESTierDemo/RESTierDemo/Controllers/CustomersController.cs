using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using RESTierDemo.Models;

namespace RESTierDemo.Controllers
{
    /*
    在为此控制器添加路由之前，WebApiConfig 类可能要求你做出其他更改。请适当地将这些语句合并到 WebApiConfig 类的 Register 方法中。请注意 OData URL 区分大小写。

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using RESTierDemo.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Customer>("Customers");
    builder.EntitySet<CustomerAddress>("CustomerAddress"); 
    builder.EntitySet<SalesOrderHeader>("SalesOrderHeader"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class CustomersController : ODataController
    {
        private AdventureWorksLT db = new AdventureWorksLT();

        // GET: odata/Customers
        [EnableQuery]
        public IQueryable<Customer> GetCustomers()
        {
            return db.Customer;
        }

        // GET: odata/Customers(5)
        [EnableQuery]
        public SingleResult<Customer> GetCustomer([FromODataUri] int key)
        {
            return SingleResult.Create(db.Customer.Where(customer => customer.CustomerID == key));
        }

        // PUT: odata/Customers(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Customer> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Customer customer = db.Customer.Find(key);
            if (customer == null)
            {
                return NotFound();
            }

            patch.Put(customer);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(customer);
        }

        // POST: odata/Customers
        public IHttpActionResult Post(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customer.Add(customer);
            db.SaveChanges();

            return Created(customer);
        }

        // PATCH: odata/Customers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Customer> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Customer customer = db.Customer.Find(key);
            if (customer == null)
            {
                return NotFound();
            }

            patch.Patch(customer);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(customer);
        }

        // DELETE: odata/Customers(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Customer customer = db.Customer.Find(key);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customer.Remove(customer);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Customers(5)/CustomerAddress
        [EnableQuery]
        public IQueryable<CustomerAddress> GetCustomerAddress([FromODataUri] int key)
        {
            return db.Customer.Where(m => m.CustomerID == key).SelectMany(m => m.CustomerAddress);
        }

        // GET: odata/Customers(5)/SalesOrderHeader
        [EnableQuery]
        public IQueryable<SalesOrderHeader> GetSalesOrderHeader([FromODataUri] int key)
        {
            return db.Customer.Where(m => m.CustomerID == key).SelectMany(m => m.SalesOrderHeader);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int key)
        {
            return db.Customer.Count(e => e.CustomerID == key) > 0;
        }
    }
}
