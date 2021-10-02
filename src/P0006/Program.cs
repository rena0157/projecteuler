// Project Euler - Problem 6
var sumOfSquares = Enumerable.Range(1, 100).Select(n => (int)Math.Pow(n, 2.0)).Sum();
var squareOfSum = (int)Math.Pow(Enumerable.Range(1, 100).Sum(), 2);
var answer = squareOfSum - sumOfSquares;
Console.WriteLine($"Answer: {answer}");