using System;

namespace App.ResourceManagement
{
    public class ControllerBase
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
        
        public override bool Equals(object? obj)
        {
            return obj switch
            {
                null => false,
                ControllerBase controllerBase => controllerBase.Identifier == Identifier,
                _ => false
            };
        }
        public override int GetHashCode() => Identifier.GetHashCode();
        public override string ToString() => $"{Identifier}_ResourceGroup";
    }
}