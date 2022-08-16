using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Management.Core;

namespace Management.Variables
{
    public class VariableContainer : IDictionary<string, IVariable?>
    {
        private readonly Dictionary<string, IVariable?> _variables = new Dictionary<string, IVariable?>();
        
        
        /* IDictionary */
        public IEnumerator<KeyValuePair<string, IVariable?>> GetEnumerator() => _variables.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(KeyValuePair<string, IVariable?> item) => Add(item.Key, item.Value);

        public void Clear() => _variables.Clear();

        public bool Contains(KeyValuePair<string, IVariable?> item) => Values.Contains(item.Value) || ContainsKey(item.Key);

        public void CopyTo(KeyValuePair<string, IVariable?>[] array, int arrayIndex) => _variables.ToArray().CopyTo(array, arrayIndex);

        public bool Remove(KeyValuePair<string, IVariable?> item)
        {
            var variable = DeepSearch(new Namespace(item.Key));
            if (variable == null) return false;
            variable.Parent?.Remove(variable.Name);
            return true;
        }

        public int Count => _variables.Count;
        public bool IsReadOnly => false;
        public void Add(string key, IVariable? value) => Add(key, value as object);

        public void Add(string key, object? value)
        {
            Namespace nsMain = new Namespace(key);
            Add(nsMain, value);
        }

        public void Add(Namespace ns, object? value)
        {
            IVariable? iVariable = DeepSearch(ns);
            if (ns.NamespaceEnd)
            {
                if (value is Variable variableVal)
                    _variables.Add(ns.CurrentSpace, variableVal);
                else
                    _variables.Add(ns.CurrentSpace, new Variable(ns, this){ Value = value });
                return;
            }
            
            if (iVariable == null || iVariable is Variable && !ns.NamespaceEnd)
            {
                iVariable = new VariableGroup(ns, this);
                _variables.Add(ns.CurrentSpace, iVariable);
            }

            ns.Next(out string key);

            switch (iVariable)
            {
                case VariableGroup vg:
                    vg.Add(ns, value);
                    break;
                case Variable variable:
                    _variables.Add(ns.CurrentSpace, variable);
                    break;
            }
        }

        public bool ContainsKey(string key) => _variables.ContainsKey(key);

        public bool Remove(string key) => _variables.Remove(key);

        public bool TryGetValue(string key, out IVariable? value)
        {
            value = DeepSearch(new Namespace(key));
            return value != null;
        }

        public IVariable? this[string key]
        {
            get => TryGetValue(key, out IVariable? value) ? value : null;
            set => Add(key, value);
        }

        public ICollection<string> Keys => _variables.Keys;
        public ICollection<IVariable?> Values => _variables.Values;
        
        
        /* Internals */
        private IVariable? GetLocal(Namespace @namespace)
        {
            if (@namespace == null) return null;
            return ContainsKey(@namespace.CurrentSpace) ? _variables[@namespace.CurrentSpace] : null;
        }

        private IVariable? DeepSearch(Namespace ns)
        {
            IVariable? variable = GetLocal(ns);
            if (variable == null) return null;
            switch (variable)
            {
                case VariableGroup container:
                    var nextSearch = ns.GetNextSearch(container);
                    if (nextSearch != null && !ns.NamespaceEnd) return nextSearch;
                    if (nextSearch == null || ns.NamespaceEnd)
                        return container;
                    return null;
                case Variable var:
                    return var;
            }
            return null;
        }
    }
}