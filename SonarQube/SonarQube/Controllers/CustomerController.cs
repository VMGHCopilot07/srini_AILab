     using Microsoft.AspNetCore.Mvc;
     using System.Collections.Generic;

     namespace InsuranceApi.Controllers
     {
         [ApiController]
         [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private static readonly List<string> Customers = new List<string> { "Customer1", "Customer2", "Customer3" };

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            return Ok(Customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            if (id < 1 || id > Customers.Count)
            {
                return NotFound("Customer not found");
            }

            var customer = Customers[id - 1];
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] string customer)
        {
            Customers.Add(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = Customers.Count }, customer);
        }
    }
     }
     