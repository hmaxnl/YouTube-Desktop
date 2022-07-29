using System.Collections.ObjectModel;
using App.ResourceManagement;

namespace App
{
    public sealed class ResourceManager
    {
        public static ObservableCollection<ISwitchingResource>
            ResourceSwitchers = new ObservableCollection<ISwitchingResource>();
        
        public static void AddResourceSwitch(ISwitchingResource switchingResource)
        {
            ResourceSwitchers.Add(switchingResource);
        }

        public static void TestLoad()
        {
            
        }
    }
}