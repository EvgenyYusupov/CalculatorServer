using System;
using System.ComponentModel.Composition;
using Calculator.Extension;

namespace ExtendedOperations
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '^')]
    public class Power : IOperation
    {
        public double Operate(int left, int right)
        {
            return Math.Pow(left, right);
        }
    }
}