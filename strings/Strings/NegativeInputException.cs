namespace Strings;

/// <summary>
/// Exception to throw when the input contains negative numbers
/// </summary>
#pragma warning disable CA1032 // Implement standard exception constructors
public class NegativeInputException : Exception
#pragma warning restore CA1032 // Implement standard exception constructors
{
    /// <summary>
    /// The negative values that caused the error
    /// </summary>
    public List<int> ExceptionValues { get; } = [];


    /// <summary>
    /// Default
    /// </summary>
    /// <param name="exceptionValues">The negative values that caused the error</param>
    public NegativeInputException(List<int> exceptionValues) : base($"negatives not allowed - [{string.Join(",", exceptionValues)}]") => ExceptionValues = exceptionValues;
    /// <summary>
    /// Set custom message
    /// </summary>
    /// <param name="message">Custom message</param>
    /// <param name="exceptionValues">The negative values that caused the error</param>
    public NegativeInputException(string message, List<int> exceptionValues) : base(message) => ExceptionValues = exceptionValues;
    /// <summary>
    /// Set custom message and inner exception
    /// </summary>
    /// <param name="message">Custom message</param>
    /// <param name="inner">Inner exception</param>
    /// <param name="exceptionValues">The negative values that caused the error</param>
    public NegativeInputException(string message, Exception inner, List<int> exceptionValues) : base(message, inner) => ExceptionValues = exceptionValues;
    
}
