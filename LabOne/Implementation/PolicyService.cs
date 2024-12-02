using LabOne.Controllers;
using LabOne.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LabOne.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly PolicyContext _context;

        public PolicyService(PolicyContext context)
        {
            _context = context;
        }

        public IEnumerable<Policy> GetAllPolicies()
        {
            return _context.Policies.ToList();
        }

        public Policy GetPolicyById(int id)
        {
            return _context.Policies.Find(id);
        }

        public void AddPolicy(Policy policy)
        {
            _context.Policies.Add(policy);
            _context.SaveChanges();
        }

        public void UpdatePolicy(Policy policy)
        {
            _context.Entry(policy).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeletePolicy(int id)
        {
            var policy = _context.Policies.Find(id);
            if (policy != null)
            {
                _context.Policies.Remove(policy);
                _context.SaveChanges();
            }
        }
        /// <summary>
        /// Gets the middle name of the policyholder by policy ID.
        /// </summary>
        /// <param name="policyId">The policy ID.</param>
        /// <returns>The middle name of the policyholder.</returns>
        public string GetMiddleName(int policyId)
        {
            var policy = _context.Policies.Find(policyId);
            if (policy == null)
            {
                throw new KeyNotFoundException("Policy not found.");
            }
            return policy.MiddleName;
        }

        public IEnumerable<string> listOfEmailIds()
        {
            return _context.Policies.Select(p => p.Email).ToList();
        }
    }
}
