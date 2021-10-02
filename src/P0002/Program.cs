// Project Euler Problem 2

const int givenMaxValue = 4_000_000;

IEnumerable<int> GetFibonacci(int max)
{
    var previous = 0;
    var current = 1;
    
    while (true)
    {
        int next;
        checked { next = current + previous; }
        
        if (next > max) yield break;
        
        previous = current;
        current = next;
        
        yield return next;
    }
}

var answer = GetFibonacci(givenMaxValue).Where(n => n % 2 == 0).Sum();
Console.WriteLine($"P0002 Solution: {answer}");