using Amazon.Lambda.Core;
using salesLambdaFunction.Models;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace salesLambdaFunction;

public class Function
{
    private readonly decimal _baseTax = .10M;
    private readonly decimal _importTax = .05M;
    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    public OutputModel FunctionHandler(List<InputModel> input, ILambdaContext context)
    {
        var outputItems = new List<OutputItem>();
        
        List<string> noTaxTypes = ["book", "medical", "food"];
        var total = 0M;
        var totalTax = 0M;
        foreach(var item in input)
        {
            var price = item.Price;
            var tax = 0M;
            if(!noTaxTypes.Contains(item.type.ToLower()))
            {
                tax += price * _baseTax;
            }
            if(item.IsImported)
            {
                tax += price * _importTax;
            }
            tax = RoundUpToNearest0_05(tax);
            total += (price + tax) * item.Quantity;
            totalTax += tax * item.Quantity;
            outputItems.Add(new OutputItem(item.Name, item.Quantity, (price + tax) * item.Quantity, item.IsImported));
        }
        return new OutputModel(outputItems, total, totalTax);
    }
    
    private static decimal RoundUpToNearest0_05(decimal value)
    {
        return Math.Ceiling(value * 20) / 20M;
    }
}
