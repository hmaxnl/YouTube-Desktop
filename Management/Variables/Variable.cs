using Management.Core;

namespace Management.Variables
{
    public class Variable : IVariable
    {
        public Variable(Namespace @namespace, VariableContainer parent, VariableFlags flags = VariableFlags.Undefined)
        {
            Namespace = @namespace.GetCurrentNamespace();
            Name = @namespace.CurrentSpace;
            Parent = parent;
            Flags = flags;
        }

        public string Namespace { get; }
        public string Name { get; }
        public VariableContainer? Parent { get; }

        public VariableFlags Flags { get; set; }

        public object? Value { get; set; }
    }
}