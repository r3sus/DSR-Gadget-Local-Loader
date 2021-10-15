using DSR_Gadget.List_Items;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DSR_Gadget
{
    public class DSRGood : IDSRProperty
    {
        private static Regex goodEntryRx = new Regex(@"^\s*(?<ID>\S+)\s+(?<limit>\S+)\s+(?<upgrade>\S+)\s+(?<Name>.+)$");

        public string Name { get; set; }
        public int ID { get; set;  } 

        private DSRGood(string config)
        {
            Match goodEntry = goodEntryRx.Match(config);
            Name = goodEntry.Groups["Name"].Value;
            ID = Convert.ToInt32(goodEntry.Groups["ID"].Value);
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(IDSRProperty other)
        {
            return Name.CompareTo(other.Name);
        }

        public DSRGood(string name, int id)
        {
            Name = name;
            ID = id;
        }

        public DSRGood()
        {
            Name = "";
            ID = -1;
        }

        //public static List<DSRProtector> All = new List<DSRProtector>();
        public static Dictionary<int, string> Dict = new Dictionary<int, string>();

        static DSRGood()
        {
            foreach (var category in DSRItemCategory.All)
            {
                if (category.Name.Replace(" ", "").Contains("UsableItems"))
                {
                    foreach (var item in category.Items)
                        Dict.Add(item.ID, item.Name);
                }
                else if (category.Name.Replace(" ", "").Contains("MysteryGoods"))
                {
                    foreach (var item in category.Items)
                        Dict.Add(item.ID, item.Name);
                }
                else if (category.Name.Replace(" ", "").Contains("Consumables"))
                {
                    foreach (var item in category.Items)
                        Dict.Add(item.ID, item.Name);
                }

            }
            
            Dict.Add(-1, "None");
        }
    }
}
