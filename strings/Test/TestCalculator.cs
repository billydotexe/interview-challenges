using Strings;

namespace Test;

public class TestCalculator
{

    [Theory]
    [InlineData("", 0)]
    [InlineData("1", 1)]
    [InlineData("1,2", 3)]
    [InlineData("1,2,3", 6)]
    [InlineData("1,2,3,4,5,6,7,8", 36)]
    public void Calculator_Add_ShouldAdd_CommaSeparated(string input, int expected)
    {
        // Act
        var result = Calculator.Add(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("-1", -1)]
    [InlineData("1,-2", -2)]
    [InlineData("1,2,-3", -3)]
    [InlineData("1,2,3,4,-5,6,7,-8", -5, -8)]
    public void Calculator_Add_ShouldThrow_OnNegativeInputs(string input, params int[] errors)
    {
        // Act
        var exception = Assert.Throws<NegativeInputException>(() => Calculator.Add(input));

        //Assert
        Assert.Equal($"negatives not allowed - [{string.Join(",", errors)}]", exception.Message);
        Assert.Equal(exception.ExceptionValues, errors);
    }


    [Theory]
    [InlineData("1000", 0)]
    [InlineData("1,1000", 1)]
    [InlineData("1,2,12344", 3)]
    [InlineData("1,2,3,6969", 6)]
    [InlineData("1,2,3,4,5,6,7,8,42069", 36)]
    public void Calculator_Add_ShouldIgnore_BiggerThan1000(string input, int expected)
    {
        // Act
        var result = Calculator.Add(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("//[**]//", 0)]
    [InlineData("//[**]//1", 1)]
    [InlineData("//[**]//1**2", 3)]
    [InlineData("//[.][,]//1.2,3", 6)]
    [InlineData("//[,][.][|||]//1,2,3.4.5,6|||7,8", 36)]
    public void Calculator_Add_ShouldUse_CustomDelimiters(string input, int expected)
    {
        // Act
        var result = Calculator.Add(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
