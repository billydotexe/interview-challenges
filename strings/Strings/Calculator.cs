using System.Text.RegularExpressions;

namespace Strings;

/// <summary>
/// Class that parse strings with delimiters to calculate basic operations with the parsed numbers
/// </summary>
public static partial class Calculator
{
    /// <summary>
    /// Adds the numbers separated by delimiters inside the input string.
    /// Delimiters can be specified with the format //[delimiter1][delimiter2]//
    /// Default delimiter: ,
    /// </summary>
    /// <param name="input">Input string</param>
    /// <returns>the extracted numbers added</returns>
    /// <exception cref="NegativeInputException"></exception>
    public static int Add(string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return 0;
        }

        var result = 0;
        var errors = new List<int>();

        var delimiters = GetDelimiters(ref input);
        var inputs = input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

        foreach(var number in inputs)
        {
            if(!IsValidNumber(errors, number, out var value))
            {
                continue;
            }

            result += value;
        }
        return errors.Count > 0 ? throw new NegativeInputException(errors) : result;
    }

    /// <summary>
    /// A valid number must be a number, positive and less than 1000
    /// </summary>
    /// <param name="errors">List that will contain possible errors</param>
    /// <param name="number">Number to be checked</param>
    /// <param name="res" default="0">Possible pared number</param>
    /// <returns>If the value was valid</returns>
    private static bool IsValidNumber(List<int> errors, string number, out int res)
    {
        res = 0;
        if (!int.TryParse(number, out var value) || value >= 1000)
        {
            return false;
        }
        if (value < 0)
        {
            errors.Add(value);
            return false;
        }

        res = value;
        return true;
    }

    /// <summary>
    /// Parses the string to extract the delimiters based on the pattern //[delimiter1][delimiter2]..//input
    /// </summary>
    /// <param name="input">Input stream</param>
    /// <returns>The parsed delimiters</returns>
    private static string[] GetDelimiters(ref string input)
    {
        var splitInput = input.Split("//", StringSplitOptions.RemoveEmptyEntries);
        // the input either doesn't match the pattern, has only delimiters or is empty, return the default delimiter (,)
        if (!MyRegex().IsMatch(input) || splitInput.Length <= 1)
        {
            return [","];
        }

        var rawDelimiters = splitInput[0];
        //skip first and last characters since they surely are [ and ]
        var delimiters = rawDelimiters[1..^1].Split("][", StringSplitOptions.RemoveEmptyEntries);

        //no delimiters extracted, return the default delimiter (,)
        if(delimiters.Length == 0)
        {
            return [","];
        }

        //updating the input string removing the delimiters from it
        input = splitInput[1];
        return delimiters;
    }

    [GeneratedRegex(@"^//(.*?)//")]
    private static partial Regex MyRegex();
}
