/* Project Euler: Problem 1
 *
 * If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9.
 * The sum of these multiples is 23. Find the sum of all the multiples of 3 or 5 below 1000.
 * 
 */

const int givenStart = 0;
const int givenEnd = 1000;

IEnumerable<int> GetNaturalNumbers(int start, int end)
{
    var current = start;

    while (current < end)
        yield return current++;
}

var sum = GetNaturalNumbers(givenStart, givenEnd).Where(n => n % 3 == 0 || n % 5 == 0).Sum();

Console.WriteLine($"P0001 Solution: {sum}");