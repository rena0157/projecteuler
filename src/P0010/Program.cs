
IEnumerable<long> GetNumbers(long max)    
{    
    var current = 0;    
    while (current < max)    
        yield return current++;    
}

bool IsPrime(long number)  
{  
    switch (number)  
    {  
        case < 2:  
            return false;  
        case 2:  
            return true;  
    }  
  
    if (number % 2 == 0)  
        return false;    
        
    for (var factor = 3; factor <= (long)Math.Sqrt(number); factor++)    
    {    
        if (number % factor == 0)    
            return false;    
    }    
    
    return true;    
}

long Solution(long maxPrime)
{
    var sum = 0L;
    
    foreach (var number in GetNumbers(long.MaxValue))
    {
        if (!IsPrime(number))
            continue;

        if (number >= maxPrime)
            return sum;
        
        sum += number;
    }

    throw new Exception("Sum not reached");
}

var solution = Solution(2_000_000);
Console.WriteLine($"P0010 Solution: {solution}");