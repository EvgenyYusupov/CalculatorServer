namespace Calculator.Extension
{
    public interface ICalculator
    {
        //string Calculate(string input);
        string Calculate(char operation, int left, int right);
        System.Collections.Generic.List<string> GetOperations();
    }
}