
(int, int, int) GetFactors()
{
    for (var a = 1; a < 1000; a++)
    {
        for (var b = a + 1; b < 1000; b++)
        {
            for (var c = b + 1; c < 1000; c++)
            {
                var sum = a + b + c;

                if (sum == 1000 && a * a + b * b == c * c)
                    return (a, b, c);

                if (sum > 1000)
                    break;
            }
        }
    }

    throw new Exception("No Solution Found");
}

var factors = GetFactors();
var product = factors.Item1 * factors.Item2 * factors.Item3;

Console.WriteLine($"P0009 Solution: {product}");