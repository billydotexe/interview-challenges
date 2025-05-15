namespace salesLambdaFunction.Models;

public record InputModel
(
    string Name,
    string type,
    decimal Price,
    int Quantity,
    bool IsImported
);