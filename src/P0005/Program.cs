﻿// Project Euler - Problem 5

using System;
using System.Linq;

bool IsDivisableBy(int number, params int[] divisors)
{
    return divisors.All(divisor => number % divisor == 0);
}

int Solution(int maxSearchNumber, Range range)
{
    var divisors = Enumerable.Range(range.Start.Value, range.End.Value).ToArray();
    
    foreach (var number in Enumerable.Range(1, maxSearchNumber))
    {
        if (IsDivisableBy(number, divisors))
            return number;
    }

    throw new Exception("No Solution Found");
}

Console.WriteLine(Solution(int.MaxValue, 1..20));