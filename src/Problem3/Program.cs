﻿using System;
using System.Collections.Generic;
using System.Linq;

// Project Euler - Problem 3

IEnumerable<long> GetNumbers(long max, long min = 0)
{
    var current = min;
    while (current < max)
        yield return current++;
}

bool IsPrime(long number)
{
    if (number % 2 == 0)
        return false;
    
    for (var factor = 3; factor < Math.Sqrt(number); factor++)
    {
        if (number % factor == 0)
            return false;
    }

    return true;
}

bool IsFactor(long number, long suspectedFactor) => number % suspectedFactor == 0;

IEnumerable<long> GetPrimeFactors(long number)
{
    var totalLeftBranchFactors = 1L;
    
    foreach (var leftBranch in GetNumbers(number, 2))
    {
        if (!IsFactor(number, leftBranch)) 
            continue;
        
        if (!IsPrime(leftBranch))
            continue;
        
        var rightBranch = number / totalLeftBranchFactors;

        if (IsPrime(rightBranch))
        {
            yield return rightBranch;
            yield break;
        }

        totalLeftBranchFactors *= leftBranch;
        yield return leftBranch;
    }
}

var factors = GetPrimeFactors(600_851_475_143);
var largestFactor = factors.Max();
Console.WriteLine($"Project Euler - Problem 3 Answer: {largestFactor}");
