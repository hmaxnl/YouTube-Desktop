namespace Management.Variables
{
    public interface IVariable
    {
        public string Namespace { get; }
        public string Name { get; }
        public VariableContainer? Parent { get; }
    }
}