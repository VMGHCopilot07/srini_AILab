public class UnderwritingEngine
{
    private List<UnderwritingRule> rules = new List<UnderwritingRule>();

    public void AddRule(UnderwritingRule rule)
    {
        rules.Add(rule);
    }

    public UnderwritingResult Evaluate(PolicyHolder policyHolder)
    {
        foreach (var rule in rules)
        {
            if (!rule.Condition(policyHolder))
            {
                return new UnderwritingResult
                {
                    IsApproved = false,
                    Reason = rule.FailureReason,
                    Premium = 0
                };
            }
        }

        return new UnderwritingResult
        {
            IsApproved = true,
            Reason = "Approved",
            Premium = CalculatePremium(policyHolder)
        };
    }

    private double CalculatePremium(PolicyHolder policyHolder)
    {
        // Complex premium calculation logic
        return policyHolder.Policies.Count * 100;
    }
}
