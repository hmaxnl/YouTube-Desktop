using System.Linq;
using Management.Variables;
using Newtonsoft.Json.Linq;

namespace Management
{
    /// <summary>
    /// Some variable management thingy.
    /// </summary>
    public static class Registry
    {
        private static readonly VariableContainer VariableContainer = new VariableContainer();
        public static VariableContainer Variables => VariableContainer;
        
        /// <summary>
        /// Register a variable.
        /// </summary>
        /// <param name="property">The property namespace.</param>
        /// <param name="value">The value.</param>
        public static void RegisterVariable(string property, object? value)
        {
            if (string.IsNullOrEmpty(property)) return;
            VariableContainer.Add(property, value);
        }

        /// <summary>
        /// Get the value in a Variable object. If non existing returns null!
        /// </summary>
        /// <param name="property">Property namespace.</param>
        /// <returns></returns>
        public static object? GetValue(string property)
        {
            IVariable? varOut = GetVariable(property);
            if (varOut is Variable var)
                return var.Value;
            return null;
        }

        /// <summary>
        /// Get the IVariable object.
        /// </summary>
        /// <param name="name">The namespace the object lives in.</param>
        /// <returns></returns>
        public static IVariable? GetVariable(string name) => !VariableContainer.TryGetValue(name, out IVariable? varOut) ? null : varOut;

        /// <summary>
        /// Merge app variables with the applied container.
        /// </summary>
        /// <param name="container">The container to be merged.</param>
        /// <returns></returns>
        public static bool Merge(VariableContainer container)
        {
            if (container == null || container.Count == 0) return false;
            container.ToList().ForEach(x => VariableContainer[x.Key] = x.Value);
            return true;
        }

        //TODO: Further implement this feature!
        public static JObject ToJson() => new JObject(VariableContainer);
    }
}