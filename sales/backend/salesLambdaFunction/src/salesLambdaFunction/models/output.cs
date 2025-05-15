namespace salesLambdaFunction.Models;

public record OutputModel
(
    List<OutputItem> Items,
    decimal Total,
    decimal Tax
);

public record OutputItem
(
    string Name,
    int Quantity,
    decimal Price,
    bool IsImported
);