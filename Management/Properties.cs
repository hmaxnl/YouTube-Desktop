using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Management
{
    /// <summary>
    /// Simple property system.
    /// </summary>
    public class Properties : PropertyContainer
    {
        public string PropertiesPath
        {
            get
            {
                if (string.IsNullOrEmpty(_propPath)) throw new Exception("No path specified!");
                return _propPath;
            }
            set => _propPath = value;
        }
        private string? _propPath;
        
        public Result Save()
        {
            if (string.IsNullOrEmpty(PropertiesPath)) return new Result(ResultCode.Error, "No path specified!");
            File.WriteAllText(PropertiesPath, ToJson().ToString());
            return new Result(ResultCode.Ok);
        }

        public Result Load(PropertyContainer container = null!)
        {
            if (container != null)
                Merge(container);
            if (string.IsNullOrEmpty(PropertiesPath)) return new Result(ResultCode.Warning, "No path specified!");
            string json;
            try
            {
                json = File.ReadAllText(PropertiesPath);
            }
            catch (Exception e)
            {
                Save();
                return new Result(ResultCode.Error, e.Message);
            }
            Fromjson(json);
            return new Result(ResultCode.Ok, "Properties successfully loaded!");
        }
    }

    public struct Property
    {
        public object? Value;
        public bool Serializable;
    }

    public class PropertyContainer : IDictionary<string, object?>
    {
        public PropertyContainer(IDictionary<string, object?> properties = null!)
        {
            if (properties == null || properties.Count == 0) return;
            foreach (var property in properties)
            {
                Add(property);
            }
        }
        private readonly Dictionary<string, Property> _properties = new Dictionary<string, Property>();
        public int Count => _properties.Count;
        public bool IsReadOnly => false;

        public string GetString(string key) => this[key]?.ToString() ?? string.Empty;
        public void Add(string key, object? value) => Add(key, value, true);
        public void Add(KeyValuePair<string, object?> item) => Add(item.Key, item.Value, true);
        public void Add(string key, object? value, bool serializable)
        {
            if (_properties.TryGetValue(key, out Property property))
            {
                property.Value = value;
                _properties[key] = property;
                return;
            }
            _properties.Add(key, new Property() {Value = value, Serializable = serializable});
        }
        public bool Contains(KeyValuePair<string, object?> item) => _properties.ContainsKey(item.Key);
        public bool ContainsKey(string key) => _properties.ContainsKey(key);
        public bool Remove(string key) => _properties.Remove(key);
        public bool Remove(KeyValuePair<string, object?> item) => Contains(item) && Remove(item.Key);
        public bool TryGetValue(string key, out object? value)
        {
            value = null;
            if (!_properties.TryGetValue(key, out Property property)) return false;
            value = property.Value;
            return true;
        }
        public void Clear() => _properties.Clear();
        public void CopyTo(KeyValuePair<string, object?>[] array, int arrayIndex)
        {
            if (arrayIndex > Count) return;
            KeyValuePair<string, object?>[] thisArr = this.ToArray();
            for (int i = 0; i < Count; i++)
            {
                if (arrayIndex + i > Count) return;
                array[i] = thisArr[arrayIndex + i];
            }
        }

        public ICollection<string> Keys => _properties.Keys;
        public ICollection<object?> Values
        {
            get
            {
                List<object?> valueList = new List<object?>();
                _properties.Values.ToList().ForEach(x => valueList.Add(x.Value));
                return valueList;
            }
        }

        public IEnumerator<KeyValuePair<string, object?>> GetEnumerator()
        {
            Dictionary<string, object?> dict = new Dictionary<string, object?>();
            string[] keys = Keys.ToArray();
            object?[] values = Values.ToArray();
            for (int i = 0; i < Count; i++)
            {
                dict.Add(keys[i], values[i]);
            }
            return dict.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public object? this[string key]
        {
            get => _properties.TryGetValue(key, out Property property) ? property.Value : null;
            set => Add(key, value);
        }
        public Result Merge(PropertyContainer properties)
        {
            if (properties == null || properties.Count == 0) return new Result(ResultCode.Warning, "Merging container does not contain any properties!");
            foreach (var kvpProperty in properties._properties)
            {
                // If a property already exists we override it! 
                _properties[kvpProperty.Key] = kvpProperty.Value;
            }
            return new Result(ResultCode.Ok);
        }

        private ReadOnlyDictionary<string, object?> SerializableProperties =>
            new ReadOnlyDictionary<string, object?>(_properties.Where(property => property.Value.Serializable)
                .ToDictionary(property => property.Key, property => property.Value.Value));

        public JObject ToJson() => JObject.Parse(JsonConvert.SerializeObject(SerializableProperties));

        public Result Fromjson(string json)
        {
            if (string.IsNullOrEmpty(json)) return new Result(ResultCode.Error, "The provided JSON is not valid!");
            ReadOnlyDictionary<string, object?> readOnlyDict =
                new ReadOnlyDictionary<string, object?>(JsonConvert.DeserializeObject<Dictionary<string, object?>>(json)!);
            Merge(new PropertyContainer(readOnlyDict));
            return new Result(ResultCode.Ok);
        }
    }
}