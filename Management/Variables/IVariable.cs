using System;

namespace Management.Variables
{
    public interface IVariable
    {
        public string Namespace { get; }
        public string Name { get; }
        public VariableContainer? Parent { get; }
        public VariableFlags Flags { get; set; }
    }

    [Flags]
    public enum VariableFlags
    {
        Undefined = 0,
        /// <summary>
        /// Only set value once. And unwritable to disk!
        /// </summary>
        Locked = 1 << 1
    }
}