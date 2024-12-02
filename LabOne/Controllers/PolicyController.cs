using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using log4net;
using LabOne.Services;

namespace LabOne.Controllers

{
    /// <summary>
    /// Controller for managing policies.
    /// </summary>
    [Authorize]
    public class PolicyController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(PolicyController));

        private readonly IPolicyService _policyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyController"/> class.
        /// </summary>
        /// <param name="policyService">The policy service.</param>
        public PolicyController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        // Existing methods

        [HttpGet]
        [Route("api/policies/{id}")]
        public IHttpActionResult GetPolicyById(int id)
        {
            var policy = _policyService.GetPolicyById(id);
            if (policy == null)
            {
                return NotFound();
            }
            return Ok(policy);
        }

        [HttpGet]
        [Route("api/policies")]
        public IHttpActionResult GetAllPolicies()
        {
            var policies = _policyService.GetAllPolicies();
            return Ok(policies);
        }

        [HttpPost]
        [Route("api/policies")]
        public IHttpActionResult AddPolicy([FromBody] Policy policy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _policyService.AddPolicy(policy);
            return CreatedAtRoute("DefaultApi", new { id = policy.PolicyId }, policy);
        }

        [HttpPut]
        [Route("api/policies/{id}")]
        public IHttpActionResult UpdatePolicy(int id, [FromBody] Policy policy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != policy.PolicyId)
            {
                return BadRequest();
            }
            _policyService.UpdatePolicy(policy);
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("api/policies/{id}")]
        public IHttpActionResult DeletePolicy(int id)
        {
            if (_policyService.GetPolicyById(id)!=null)
            {
                return NotFound();
            }
            _policyService.DeletePolicy(id);
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        // New method

        /// <summary>
        /// Gets the middle name of the policyholder by policy ID.
        /// </summary>
        /// <param name="id">The policy ID.</param>
        /// <returns>The middle name of the policyholder.</returns>
        [HttpGet]
        [Route("api/policies/{id}/middlename")]
        public IHttpActionResult GetMiddleName(int id)
        {
            try
            {
                var middleName = _policyService.GetMiddleName(id);
                return Ok(middleName);
            }
            catch (KeyNotFoundException ex)
            {
                logger.Error($"Policy with ID {id} not found.", ex);
                return NotFound();
            }
            catch (Exception ex)
            {
                logger.Error($"Error occurred while getting middle name for policy with ID {id}.", ex);
                return InternalServerError(ex);
            }
        }
        
        
        [HttpGet]
        [Route("api/policies/emails")]
        public IHttpActionResult GetListOfEmailIds()
        {
            try
            {
                var emailIds = _policyService.listOfEmailIds();
                return Ok(emailIds);
            }
            catch (Exception ex)
            {
                logger.Error("Error occurred while getting list of email IDs.", ex);
                return InternalServerError(ex);
            }
        }

        // Generate method to retun the user namae from policyid

        [HttpGet]
        [Route("api/policies/{id}/username")]
        public IHttpActionResult GetUserName(int id)
        {
            try
            {
                var policy = _policyService.GetPolicyById(id);
                if (policy == null)
                {
                    return NotFound();
                }
                return Ok($"{policy.FirstName} {policy.LastName}");
            }
            catch (Exception ex)
            {
                logger.Error($"Error occurred while getting user name for policy with ID {id}.", ex);
                return InternalServerError(ex);
            }
        }
    }


}
