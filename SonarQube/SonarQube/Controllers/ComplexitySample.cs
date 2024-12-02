using System;
using System.Collections.Generic;

public class ComplexSample
{
    public string ProcessData(List<int> numbers)
    {
        if (numbers == null || numbers.Count == 0)
        {
            return "No data provided";
        }

        string result = string.Empty;

        foreach (var number in numbers)
        {
            result += ProcessNumber(number);
        }

        if (string.IsNullOrEmpty(result))
        {
            return "No valid numbers processed";
        }

        return result;
    }

    private string ProcessNumber(int number)
    {
        return number switch
        {
            < 0 => "Negative number found\n",
            0 => "Zero found\n",
            > 0 and <= 20 => ProcessValidNumber(number),
            _ => "Number out of range: " + number + "\n"
        };
    }

    private string ProcessValidNumber(int number)
    {
        string numberType = number % 2 == 0 ? "Even" : "Odd";
        string range = number <= 10 ? "" : " greater than 10";
        return $"{numberType} number{range}: {number}\n";
    }
}

public class Program
{
    public static void Main()
    {
        var complexSample = new ComplexSample();
        var numbers = new List<int> { 1, 2, 3, 11, 12, 13, -1, 0, 21 };

        string result = complexSample.ProcessData(numbers);
        Console.WriteLine(result);
    }
}
