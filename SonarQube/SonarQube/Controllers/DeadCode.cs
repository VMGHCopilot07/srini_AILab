using System;
using System.Collections.Generic;
using System.Linq;

public class UnderwritingResult
{
    public bool IsApproved { get; set; }
    public string Reason { get; set; }
    public double Premium { get; set; }
}

public class UnderwritingRule
{
    public string RuleName { get; set; }
    public Func<PolicyHolder, bool> Condition { get; set; }
    public string FailureReason { get; set; }
}



