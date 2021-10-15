using DSR_Gadget.List_Items;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DSR_Gadget
{
    public class DSRAccessory : IDSRProperty
    {
        private static Regex accessoryEntryRx = new Regex(@"^\s*(?<ID>\S+)\s+(?<limit>\S+)\s+(?<upgrade>\S+)\s+(?<Name>.+)$");

        public string Name { get; set; }
        public int ID { get; set;  } 

        private DSRAccessory(string config)
        {
            Match accessoryEntry = accessoryEntryRx.Match(config);
            Name = accessoryEntry.Groups["Name"].Value;
            ID = Convert.ToInt32(accessoryEntry.Groups["ID"].Value);
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(IDSRProperty other)
        {
            return Name.CompareTo(other.Name);
        }

        public DSRAccessory(string name, int id)
        {
            Name = name;
            ID = id;
        }

        public DSRAccessory()
        {
            Name = "";
            ID = -1;
        }

        //public static List<DSRProtector> All = new List<DSRProtector>();
        public static Dictionary<int, string> Dict = new Dictionary<int, string>();

        static DSRAccessory()
        {
            foreach (var category in DSRItemCategory.All)
            {
                if (category.Name.Replace(" ", "").Contains("Rings"))
                {
                    foreach (var item in category.Items)
                        Dict.Add(item.ID, item.Name);
                }
            }
            
            Dict.Add(-1, "None");
        }
    }
}
