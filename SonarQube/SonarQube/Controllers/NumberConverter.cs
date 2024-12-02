using System;
using System.Collections.Generic;
using System.Linq;

public class NumberConverter
{
    private static readonly Dictionary<int, string> NumberToWords = Enumerable.Range(1, 10)
        .ToDictionary(
            number => number,
            number => number switch
            {
                1 => "One",
                2 => "Two",
                3 => "Three",
                4 => "Four",
                5 => "Five",
                6 => "Six",
                7 => "Seven",
                8 => "Eight",
                9 => "Nine",
                10 => "Ten",
                _ => throw new ArgumentOutOfRangeException()
            });

    public string IntegerToEnglishValue(int number)
    {
        if (NumberToWords.TryGetValue(number, out var word))
        {
            return word;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(number), "Number must be between 1 and 10");
        }
    }
}
