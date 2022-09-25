using System;
using System.Collections.Generic;
using Management.Variables;

namespace Management.Core
{
    public class Namespace
    {
        public Namespace(string @namespace)
        {
            if (string.IsNullOrEmpty(@namespace)) throw new ArgumentNullException(@namespace, "Namespace cannot be null or empty!");
            if (@namespace.Contains('.'))
            {
                Spaces.AddRange(@namespace.Split('.'));
                return;
            }
            Spaces.Add(@namespace);
            _locked = true;
        }

        public string CurrentSpace => Spaces[_count];
        public string ToDo => string.Join('.', Spaces.ToArray(), _count, Spaces.Count - _count);
        public string Original => string.Join('.', Spaces);
        public string GetCurrentNamespace() => string.Join('.', Spaces.ToArray(), 0, _count + 1);
        public bool NamespaceEnd => Spaces.Count == _count + 1;
        private List<string> Spaces { get; } = new List<string>();
        private bool _locked = false;

        private int _count = 0;
        public bool Next(out string value)
        {
            value = string.Empty;
            if (Spaces.Count == _count + 1 || _locked) return false;
            _count++;
            value = Spaces[_count];
            return true;
        }

        public IVariable? GetNextSearch(VariableContainer varContainer)
        {
            if (!Next(out string nextSpace)) return null;
            if (varContainer.ContainsKey(nextSpace))
                return varContainer[nextSpace];
            _count--;
            return null;
        }

        public void Lock() => _locked = true;
    }
}