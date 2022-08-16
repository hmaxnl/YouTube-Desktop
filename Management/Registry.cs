using Management.Variables;

namespace Management
{
    public static class Registry
    {
        private static readonly VariableContainer VariableContainer = new VariableContainer();
        
        public static void RegisterVariable(string property, object? value)
        {
            if (string.IsNullOrEmpty(property)) return;
            VariableContainer.Add(property, value);
        }
        

        public static void Init()
        {
            
        }
    }
}