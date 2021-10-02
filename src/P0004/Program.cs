// Project Euler - Problem 4

string ReverseString(string input)
{
    return string.Create(input.Length, input, (chars, state) =>
    {
        var position = 0;
        for (var index = state.Length - 1; index >= 0; index--)
        {
            chars[position++] = state[index];
        }
    });
}

bool IsPalindromic(long number)
{
    var numberString = number.ToString();
    var reversedString = ReverseString(numberString);
    return reversedString == numberString;
}

IEnumerable<(int Left, int Right)> GetProducts(int start, int end)
{
    for (var left = start; left < end; left++)
    {
        for (var right = left; right < end; right++)
        {
            yield return (left, right);
        }
    }
}

var palindromicNumbers = GetProducts(100, 1000)
    .Select(f => f.Left * f.Right)
    .Where(n => IsPalindromic(n))
    .ToList();

var max = palindromicNumbers.Max();
Console.WriteLine($"Max: {max}");