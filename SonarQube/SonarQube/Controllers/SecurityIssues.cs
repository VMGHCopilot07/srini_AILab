using System;
using System.Collections.Generic;

public class BillingInfo
{
    public string CreditCardNumber { get; set; }
    public string ExpiryDate { get; set; }
    public string CVV { get; set; }
    public string BillingAddress { get; set; }
    public string PolicyHolderName { get; set; }
}

public class BillingRecord
{
    public int RecordId { get; set; }
    public string PolicyNumber { get; set; }
    public double Amount { get; set; }
    public DateTime BillingDate { get; set; }
    public BillingInfo BillingInfo { get; set; }
}

public class BillingService
{
    private List<BillingRecord> billingRecords = new List<BillingRecord>();

    public void AddBillingRecord(BillingRecord record)
    {
        if (record == null)
        {
            throw new ArgumentException("BillingRecord cannot be null");
        }
        billingRecords.Add(record);
    }

    public void ProcessBilling()
    {
        foreach (var record in billingRecords)
        {
            ProcessSingleBilling(record);
        }
    }

    private void ProcessSingleBilling(BillingRecord record)
    {
        LogBillingInfo(record, false);
        SimulateBillingProcess(record);
    }

    private void SimulateBillingProcess(BillingRecord record)
    {
        Console.WriteLine($"Billing {record.Amount} on {record.BillingDate}");
    }

    public void PrintBillingRecords()
    {
        foreach (var record in billingRecords)
        {
            LogBillingInfo(record, true);
        }
    }

    private void LogBillingInfo(BillingRecord record, bool includePolicyNumber)
    {
        if (includePolicyNumber)
        {
            Console.WriteLine($"RecordId: {record.RecordId}, PolicyNumber: {record.PolicyNumber}, Amount: {record.Amount}, BillingDate: {record.BillingDate}, PolicyHolderName: {record.BillingInfo.PolicyHolderName}");
        }
        else
        {
            Console.WriteLine($"Processing billing for {record.BillingInfo.PolicyHolderName}, Amount: {record.Amount}");
        }
    }
}

public class Program
{
    public static void Main()
    {
        var billingService = new BillingService();

        var billingInfo = new BillingInfo
        {
            CreditCardNumber = "1234-5678-9012-3456",
            ExpiryDate = "12/25",
            CVV = "123",
            BillingAddress = "123 Main St, Anytown, USA",
            PolicyHolderName = "John Doe"
        };

        var billingRecord = new BillingRecord
        {
            RecordId = 1,
            PolicyNumber = "P123",
            Amount = 100.50,
            BillingDate = DateTime.Now,
            BillingInfo = billingInfo
        };

        billingService.AddBillingRecord(billingRecord);
        billingService.ProcessBilling();
        billingService.PrintBillingRecords();
    }
}

