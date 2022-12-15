using App.Controls;
using App.Models;
using App.ViewModels;
using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace App.XAML
{
    public class ModelControlSelector : IDataTemplate
    {
        
        public IControl Build(object param)
        {
            switch (param)
            {
                case HomeViewModel homeModel:
                    return new HomeModelViewControl(homeModel);
            }
            return null;
        }

        public bool Match(object data)
        {
            switch (data)
            {
                case HomeViewModel homeModel:
                    return true;
            }
            return false;
        }
    }
}