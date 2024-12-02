
using System;
using System.Collections.Generic;
using System.Linq;

public class RiskFactorCalculator
{
    private List<Policy> policies = new List<Policy>(); // Using List<Policy> instead of ArrayList
    private List<Claim> claims = new List<Claim>(); // Using List<Claim> instead of ArrayList

    public void AddPolicy(Policy policy)
    {
        if (policy == null)
        {
            throw new ArgumentException("Policy cannot be null");
        }
        policies.Add(policy);
    }

    public void AddClaim(Claim claim)
    {
        if (claim == null)
        {
            throw new ArgumentException("Claim cannot be null");
        }
        claims.Add(claim);
    }

    public Dictionary<int, double> CalculateRiskFactors() // Using Dictionary<int, double> instead of Hashtable
    {
        var riskFactors = policies.ToDictionary(
            policy => policy.Id,
            policy => claims.Where(claim => claim.PolicyId == policy.Id).Sum(claim => 1.5)
        );

        var policyIds = policies.Select(p => p.Id).ToHashSet();
        var invalidClaims = claims.Where(c => !policyIds.Contains(c.PolicyId)).ToList();

        if (invalidClaims.Any())
        {
            throw new Exception($"PolicyId {string.Join(", ", invalidClaims.Select(c => c.PolicyId))} from claims not found in policies");
        }

        return riskFactors;
    }
}

public partial class Program
{
    public static void Main(string[] args)
    {
        var calculator = new RiskFactorCalculator();

        var policy1 = new Policy { Id = 1, Name = "P123" };
        var policy2 = new Policy { Id = 2, Name = "P456" };

        calculator.AddPolicy(policy1);
        calculator.AddPolicy(policy2);

        var claim1 = new Claim { ClaimId = 1, PolicyId = 1, Amount = 1000 };
        var claim2 = new Claim { ClaimId = 2, PolicyId = 1, Amount = 2000 };
        var claim3 = new Claim { ClaimId = 3, PolicyId = 2, Amount = 1500 };

        calculator.AddClaim(claim1);
        calculator.AddClaim(claim2);
        calculator.AddClaim(claim3);

        try
        {
            var riskFactors = calculator.CalculateRiskFactors();
            foreach (var riskFactor in riskFactors)
            {
                Console.WriteLine($"PolicyId: {riskFactor.Key}, RiskFactor: {riskFactor.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

