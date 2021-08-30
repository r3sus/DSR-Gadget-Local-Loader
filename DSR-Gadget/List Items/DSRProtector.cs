using DSR_Gadget.List_Items;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DSR_Gadget
{
    public class DSRProtector : IDSRProperty
    {
        private static Regex protectorEntryRx = new Regex(@"^(?<ID>\S+) (?<Name>.+)$");

        public string Name { get; set; }
        public int ID { get; set;  } 

        private DSRProtector(string config)
        {
            Match protectorEntry = protectorEntryRx.Match(config);
            Name = protectorEntry.Groups["Name"].Value;
            ID = Convert.ToInt32(protectorEntry.Groups["ID"].Value);
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(IDSRProperty other)
        {
            return Name.CompareTo(other.Name);
        }

        public DSRProtector(string name, int id)
        {
            Name = name;
            ID = id;
        }

        public DSRProtector()
        {
            Name = "";
            ID = -1;
        }

        //public static List<DSRProtector> All = new List<DSRProtector>();
        public static Dictionary<int, string> Dict = new Dictionary<int, string>();

        static DSRProtector()
        {
            foreach (string line in Regex.Split(Properties.Resources.AllProtectors, "[\r\n]+"))
            {
                DSRProtector weapon = new DSRProtector(line);
                Dict.Add(weapon.ID, weapon.Name);
                //All.Add(new DSRProtector(line));
            }
        }
    }
}
