using Strings;

while (true)
{
    Console.Write("Input string:");
    var input = Console.ReadLine();

    Console.WriteLine($"Result: {Calculator.Add(input)}");
}