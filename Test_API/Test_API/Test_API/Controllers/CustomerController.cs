using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_API.DataContext;
using Test_API.Model;

namespace Test_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public CustomerController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        [Route("GetAllCustomer")]
        public IActionResult GetAllCustomer()
        {
            var data = context.customers.ToList();
            if (data.Count() == 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(data);
            }
        }
        [HttpPost]
        [Route("GetCustomerById/{id}")]
        public IActionResult GetCustomerById(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                var data = context.customers.Where(e => e.Id == id).SingleOrDefault();
                if (data == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(data);
                }
            }

        }
        [HttpPost]
        [Route("AddCustomer")]
        public IActionResult AddCustomer([FromBody] Customer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {

                var data = new Customer
                {
                    Name = model.Name,
                    Gender = model.Gender,

                    IsActive = model.IsActive
                };
                context.customers.Add(data);
                context.SaveChanges();
                return Ok(data);
            }
        }
        [HttpPut]
        [Route("UpdateCustomer")]
        public IActionResult UpdateCustomer([FromBody] Customer model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var data = context.customers.Where(e => e.Id == model.Id).SingleOrDefault();
                if (data == null)
                {
                    return BadRequest();
                }
                else
                {
                    data.Name = model.Name;
                    data.Gender = model.Gender;
                    data.IsActive = model.IsActive;
                    ////second way data pass
                    //var newdata = new Customer
                    //{
                    //    Id = model.Id,
                    //    Name = model.Name,
                    //    Gender = model.Gender,
                    //    IsActive = model.IsActive
                    //};
                    context.customers.Update(data);
                    context.SaveChanges();
                    return Ok();


                };

            }
        }
        [HttpDelete]
        [Route("DeleteCustomer/{id}")]
        public IActionResult DeleteCustomer(int id)
        {
           
            if(id!=0)
            {
                var data = context.customers.Where(e => e.Id == id).SingleOrDefault();
                if (data == null)
                {
                    return BadRequest();
                }
                else
                {
                    context.customers.Remove(data);
                    context.SaveChanges();
                }
            }
            else
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
