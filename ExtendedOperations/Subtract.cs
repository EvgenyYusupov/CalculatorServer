using System.ComponentModel.Composition;
using Calculator.Extension;

namespace Calculator.InnerExtensions
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '-')]
    class Subtract : IOperation
    {
        public double Operate(int left, int right)
        {
            return left - right;
        }
    }
}