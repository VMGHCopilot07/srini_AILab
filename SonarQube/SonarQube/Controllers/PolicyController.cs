     using Microsoft.AspNetCore.Mvc;
     using System.Collections.Generic;

     namespace InsuranceApi.Controllers
     {
         [ApiController]
         [Route("api/[controller]")]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;

        public PolicyController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        [HttpGet]
        public IActionResult GetAllPolicies()
        {
            var data = _policyService.GetAllPolicies();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult GetPolicyById(int id)
        {
            var data = _policyService.GetPolicyById(id);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult CreatePolicy([FromBody] string policy)
        {
            var createdPolicy = _policyService.CreatePolicy(policy);
            return CreatedAtAction(nameof(GetPolicyById), new { id = createdPolicy.Id }, createdPolicy);
        }
    }
     }
     