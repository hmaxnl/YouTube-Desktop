using System;

namespace App.ResourceManagement
{
    public class ResourceIdBase
    {
        private string _identifier = string.Empty;
        public string Identifier
        {
            get => _identifier;
            set
            {
                if (value == string.Empty)
                    throw new Exception("Value cannot be null!");
                // Only set this once!
                if (string.IsNullOrEmpty(_identifier))
                    _identifier = value;
            }
        }
    }
}