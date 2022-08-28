using Calculator.Extension;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace CalculatorService
{   
    public class ExtensibilityProvider
    {
        [Import(typeof(ICalculator))]
        private ICalculator _calculator;
        
        public ICalculator Calculator => _calculator;

        public ExtensibilityProvider(string extensionsPath)
        {   
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(GetType().Assembly));
            catalog.Catalogs.Add(new DirectoryCatalog(extensionsPath));
            var container = new CompositionContainer(catalog);

            try
            {
                container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                throw new Exception(compositionException.ToString());
            }
        }
    }

    [Export(typeof(ICalculator))]
    class Calculator : ICalculator
    {
        [ImportMany]
        IEnumerable<Lazy<IOperation, IOperationData>> _operations;

        public List<string> GetOperations()
        {
            List<string> operationList = new List<string>();
            foreach (var operate in _operations)
            {
                operationList.Add(operate.Metadata.Symbol.ToString());
            }
            return operationList;
        }

        public string Calculate(char operation, int left, int right)
        {
            foreach (var operate in _operations)
            {
                if (operate.Metadata.Symbol.Equals(operation))
                    return operate.Value.Operate(left, right).ToString(CultureInfo.InvariantCulture);
            }
            return string.Format("Operation '{0}' not declared", operation);
        }
    }
}
