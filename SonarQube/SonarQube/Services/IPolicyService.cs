public interface IPolicyService
{
    List<string> GetAllPolicies();
    string GetPolicyById(int id);
    Policy CreatePolicy(string policy);
}

public class PolicyService : IPolicyService
{
    public List<string> GetAllPolicies()
    {
        return new List<string> { "Policy1", "Policy2", "Policy3" };
    }

    public string GetPolicyById(int id)
    {
        return $"Policy{id}";
    }

    public Policy CreatePolicy(string policy)
    {
        // Assuming Policy has an Id property
        return new Policy { Id = 1, Name = policy };
    }
}

public class Policy
{
    public int Id { get; set; }
    public string Name { get; set; }
}
