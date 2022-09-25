using Management.Core;

namespace Management.Variables
{
    public class VariableGroup : VariableContainer, IVariable
    {
        public VariableGroup(Namespace @namespace, VariableContainer? parent = null, VariableFlags flags = VariableFlags.Undefined)
        {
            Namespace = @namespace.GetCurrentNamespace();
            Name = @namespace.CurrentSpace;
            Parent = parent;
            Flags = flags;
        }
        /// <summary>
        /// The parent container this IVariable is in.
        /// </summary>
        public VariableContainer? Parent { get; }
        
        public VariableFlags Flags { get; set; }

        public string Namespace { get; }
        public string Name { get; }
    }
}