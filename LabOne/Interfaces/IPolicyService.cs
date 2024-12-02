using System.Collections.Generic;

namespace LabOne.Services
{
    public interface IPolicyService
    {
        IEnumerable<Policy> GetAllPolicies();
        Policy GetPolicyById(int id);
        void AddPolicy(Policy policy);
        void UpdatePolicy(Policy policy);
        void DeletePolicy(int id);
        string GetMiddleName(int policyId);

        IEnumerable<string> listOfEmailIds();
    }
}
