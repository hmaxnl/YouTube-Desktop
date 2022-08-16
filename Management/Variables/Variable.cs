using Management.Core;

namespace Management.Variables
{
    public class Variable : IVariable
    {
        public Variable(Namespace @namespace, VariableContainer parent)
        {
            Namespace = @namespace.GetCurrentNamespace();
            Name = @namespace.CurrentSpace;
            Parent = parent;
        }

        public string Namespace { get; }
        public string Name { get; }
        public VariableContainer? Parent { get; }
        
        public object? Value { get; set; }
    }
}