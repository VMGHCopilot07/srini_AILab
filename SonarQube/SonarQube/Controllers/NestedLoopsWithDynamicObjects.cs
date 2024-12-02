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

    public void CalculatePremiums()
    {
       
            
            var premiumCalculations = new Dictionary<string, PremiumCalculation>
            {
                { "CA", new CaliforniaPremiumCalculation() },
                { "NY", new NewYorkPremiumCalculation() },
                { "TX", new TexasPremiumCalculation() },
                { "default", new DefaultPremiumCalculation() }
            };

            foreach (var policyHolder in policyHolders)
            {
                var state = policyHolder.State;
                var premiumCalculation = premiumCalculations.ContainsKey(state) ? premiumCalculations[state] : premiumCalculations["default"];

                foreach (var policy in policyHolder.Policies)
                {
                    foreach (var coverage in policy.Coverages)
                    {
                        coverage.Premium = premiumCalculation.Calculate(coverage);
                    }
                }
            }
        
    }

    public void PrintPolicyDetails()
    {
        foreach (var policyHolder in policyHolders)
        {
            Console.WriteLine($"PolicyHolder: {policyHolder.Name}, State: {policyHolder.State}");
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


public abstract class PremiumCalculation
{
    public abstract double Calculate(Coverage coverage);
}

public class CaliforniaPremiumCalculation : PremiumCalculation
{
    public override double Calculate(Coverage coverage)
    {
        // Custom calculation logic for California
        return coverage.Premium * 1.1;
    }
}

public class NewYorkPremiumCalculation : PremiumCalculation
{
    public override double Calculate(Coverage coverage)
    {
        // Custom calculation logic for New York
        return coverage.Premium * 1.2;
    }
}

public class TexasPremiumCalculation : PremiumCalculation
{
    public override double Calculate(Coverage coverage)
    {
        // Custom calculation logic for Texas
        return coverage.Premium * 1.15;
    }
}

public class DefaultPremiumCalculation : PremiumCalculation
{
    public override double Calculate(Coverage coverage)
    {
        // Default calculation logic
        return coverage.Premium * 1.05;
    }
}

public partial class Program
{
    public static void Main()
    {
        var calculator = new InsurancePremiumCalculator();

        var policyHolder1 = new PolicyHolder { Name = "John Doe", State = "CA" };
        policyHolder1.Policies.Add(new Policy
        {
            PolicyNumber = "P123",
            Coverages = new List<Coverage>
            {
                new Coverage { Type = "Health", Premium = 500 },
                new Coverage { Type = "Life", Premium = 300 }
            }
        });

        var policyHolder2 = new PolicyHolder { Name = "Jane Smith", State = "NY" };
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

        calculator.CalculatePremiums();
        calculator.PrintPolicyDetails();
    }
}