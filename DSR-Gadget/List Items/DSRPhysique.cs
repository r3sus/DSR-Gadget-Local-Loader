using DSR_Gadget.List_Items;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DSR_Gadget
{
    class DSRPhysique : IDSRProperty
    {
        private static Regex physiqueEntryRx = new Regex(@"^(?<ID>\S+) (?<Name>.+)$");

        public string Name { get; set; }
        public int ID { get; set;  } 

        private DSRPhysique(string config)
        {
            Match physiqueEntry = physiqueEntryRx.Match(config);
            Name = physiqueEntry.Groups["Name"].Value;
            ID = Convert.ToInt32(physiqueEntry.Groups["ID"].Value);
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(IDSRProperty other)
        {
            return Name.CompareTo(other.Name);
        }

        public DSRPhysique(string name, int id)
        {
            Name = name;
            ID = id;
        }

        public DSRPhysique()
        {
            Name = "";
            ID = -1;
        }

        public static List<DSRPhysique> All = new List<DSRPhysique>();

        static DSRPhysique()
        {
            foreach (string line in Regex.Split(GetTxtResourceClass.GetTxtResource("Resources/Systems/Other/Physiques.txt"), "[\r\n]+"))
            {
                if (GetTxtResourceClass.IsValidTxtResource(line)) //determine if line is a valid resource or not
                {
                    Match match = physiqueEntryRx.Match(line);
                    byte id = byte.Parse(match.Groups["ID"].Value);
                    string name = match.Groups["Name"].Value;
                    All.Add(new DSRPhysique(name, id));
                };
            }
            All.Sort();
        }
    }
}
