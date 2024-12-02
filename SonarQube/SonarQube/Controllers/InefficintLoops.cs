using System;
using System.Collections.Generic;

public class InsurancePremiumCalculator
{
    private List<PolicyHolder> policyHolders = new List<PolicyHolder>();

    public void AddPolicyHolder(PolicyHolder policyHolder)
    {
        if (policyHolder == null)
        {
            throw new ArgumentException("PolicyHolder cannot be null");
        }
        policyHolders.Add(policyHolder);
    }

    public double CalculateTotalPremium()
    {
        double totalPremium = 0;
        foreach (var policyHolder in policyHolders)
        {
            foreach (var policy in policyHolder.Policies)
            {
                foreach (var coverage in policy.Coverages)
                {
                    totalPremium += coverage.Premium;
                }
            }
        }
        return totalPremium;
    }

    public List<PolicyHolder> GetPolicyHoldersWithHighPremium(double threshold)
    {
        List<PolicyHolder> result = new List<PolicyHolder>();
        foreach (var policyHolder in policyHolders)
        {
            double totalPremium = 0;
            foreach (var policy in policyHolder.Policies)
            {
                foreach (var coverage in policy.Coverages)
                {
                    totalPremium += coverage.Premium;
                }
            }
            if (totalPremium > threshold)
            {
                result.Add(policyHolder);
            }
        }
        return result;
    }

    public void PrintPolicyDetails()
    {
        foreach (var policyHolder in policyHolders)
        {
            Console.WriteLine($"PolicyHolder: {policyHolder.Name}");
            foreach (var policy in policyHolder.Policies)
            {
                Console.WriteLine($"  Policy: {policy.PolicyNumber}");
                foreach (var coverage in policy.Coverages)
                {
                    Console.WriteLine($"    Coverage: {coverage.Type}, Premium: {coverage.Premium}");
                }
            }
        }
    }
}

public class PolicyHolder
{
    public string Name { get; set; }
    public List<Policy> Policies { get; set; } = new List<Policy>();
}

public class Policy
{
    public string PolicyNumber { get; set; }
    public List<Coverage> Coverages { get; set; } = new List<Coverage>();
}

public class Coverage
{
    public string Type { get; set; }
    public double Premium { get; set; }
}

public class Program
{
    public static void Main()
    {
        var calculator = new InsurancePremiumCalculator();

        var policyHolder1 = new PolicyHolder { Name = "John Doe" };
        policyHolder1.Policies.Add(new Policy
        {
            PolicyNumber = "P123",
            Coverages = new List<Coverage>
            {
                new Coverage { Type = "Health", Premium = 500 },
                new Coverage { Type = "Life", Premium = 300 }
            }
        });

        var policyHolder2 = new PolicyHolder { Name = "Jane Smith" };
        policyHolder2.Policies.Add(new Policy
        {
            PolicyNumber = "P456",
            Coverages = new List<Coverage>
            {
                new Coverage { Type = "Auto", Premium = 700 },
                new Coverage { Type = "Home", Premium = 400 }
            }
        });

        calculator.AddPolicyHolder(policyHolder1);
        calculator.AddPolicyHolder(policyHolder2);

        double totalPremium = calculator.CalculateTotalPremium();
        Console.WriteLine($"Total Premium: {totalPremium}");

        var highPremiumHolders = calculator.GetPolicyHoldersWithHighPremium(1000);
        Console.WriteLine("PolicyHolders with high premium:");
        foreach (var holder in highPremiumHolders)
        {
            Console.WriteLine(holder.Name);
        }

        calculator.PrintPolicyDetails();
    }
}
