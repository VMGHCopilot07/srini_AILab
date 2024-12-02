     using Microsoft.AspNetCore.Mvc;
     using System.Collections.Generic;

     namespace InsuranceApi.Controllers
     {
         [ApiController]
         [Route("api/[controller]")]
    public class ClaimsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllClaims()
        {
            var data = GetClaimsList();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult GetClaimById(int id)
        {
            var data = GetClaim(id);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult CreateClaim([FromBody] string claim)
        {
            return CreatedAtAction(nameof(GetClaimById), new { id = 1 }, claim);
        }

        private List<string> GetClaimsList()
        {
            return new List<string> { "Claim1", "Claim2", "Claim3" };
        }

        private string GetClaim(int id)
        {
            return $"Claim{id}";
        }
    }
     }
     