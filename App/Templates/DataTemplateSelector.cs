using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Metadata;

namespace App.Templates
{
    public class DataTemplateSelector : IDataTemplate
    {
        [Content]
        public List<IDataTemplate> Templates { get; } = new List<IDataTemplate>();
        
        public bool CheckBaseType { get; set; } = false;
        private DataTemplate? _template;
        public IControl Build(object param)
        {
            if (_template == null) throw new NullReferenceException();
            return _template.Build(param);
        }

        public bool Match(object data)
        {
            foreach (IDataTemplate template in Templates)
            {
                if (template is not DataTemplate dTemplate) continue;
                if (dTemplate.DataType != data.GetType() && (!CheckBaseType || dTemplate.DataType != data.GetType().BaseType)) continue;
                _template = dTemplate;
                return true;
            }
            return false;
        }
    }
}