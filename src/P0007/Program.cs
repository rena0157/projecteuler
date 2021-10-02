// Project Euler - Problem 7

bool IsPrime(int number)
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
      
    for (var factor = 3; factor <= (int)Math.Sqrt(number); factor++)  
    {  
        if (number % factor == 0)  
            return false;  
    }  
  
    return true;  
}

IEnumerable<int> GetNumbers(int max)  
{  
    var current = 0;  
    while (current < max)  
        yield return current++;  
}


var answer = GetNumbers(int.MaxValue)
     .Where(IsPrime)
     .ElementAt(10_000);

Console.WriteLine(answer);