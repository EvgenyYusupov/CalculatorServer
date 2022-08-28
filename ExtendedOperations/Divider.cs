using System.ComponentModel.Composition;
using Calculator.Extension;

namespace ExtendedOperations
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '/')]
    public class Divider : IOperation
    {
        public double Operate(int left, int right)
        {
            if (right == 0)
                return 0;
            return left / right;
        }
    }
}