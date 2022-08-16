using Management.Core;

namespace Management.Variables
{
    public class VariableGroup : VariableContainer, IVariable
    {
        public VariableGroup(Namespace @namespace, VariableContainer? parent = null)
        {
            Namespace = @namespace.GetCurrentNamespace();
            Name = @namespace.CurrentSpace;
            Parent = parent;
        }
        /// <summary>
        /// The parent group this IVariable is in.
        /// </summary>
        public VariableContainer? Parent { get; }

        public string Namespace { get; }
        public string Name { get; }
    }
}