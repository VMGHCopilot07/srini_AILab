using System;
using System.Collections.Generic;

public class InsurancePolicy
{
    public int PolicyId { get; set; }
    public string PolicyNumber { get; set; }
    public List<Claim> Claims { get; set; } = new List<Claim>();

    public void AddClaim(Claim claim)
    {
        claim.Policy = this;
        Claims.Add(claim);
    }
}

public class Claim
{
    public int ClaimId { get; set; }
    public int PolicyId { get; set; }
    public double Amount { get; set; }
    public InsurancePolicy Policy { get; set; }
}

public partial class Program
{
    public static void Main()
    {
        InsurancePolicy policy = new InsurancePolicy { PolicyId = 1, PolicyNumber = "P12345" };
        Claim claim1 = new Claim { ClaimId = 1, PolicyId = 1, Amount = 1000.0 };
        Claim claim2 = new Claim { ClaimId = 2, PolicyId = 1, Amount = 2000.0 };

        policy.AddClaim(claim1);
        policy.AddClaim(claim2);

        Console.WriteLine($"Policy: {policy.PolicyNumber}");
        foreach (var claim in policy.Claims)
        {
            Console.WriteLine($"Claim ID: {claim.ClaimId}, Amount: {claim.Amount}, Policy Number: {claim.Policy.PolicyNumber}");
        }
    }
}
