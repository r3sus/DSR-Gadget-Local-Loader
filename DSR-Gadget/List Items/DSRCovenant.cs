using DSR_Gadget.List_Items;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DSR_Gadget
{
    class DSRCovenant : IDSRProperty
    {
        private static Regex covenantEntryRx = new Regex(@"^(?<ID>\S+) (?<Name>.+)$");

        public string Name { get; set; }
        public int ID { get; set; }

        private DSRCovenant(string config)
        {
            Match covenantEntry = covenantEntryRx.Match(config);
            Name = covenantEntry.Groups["Name"].Value;
            ID = Convert.ToInt32(covenantEntry.Groups["ID"].Value);
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(IDSRProperty other)
        {
            return Name.CompareTo(other.Name);
        }

        public DSRCovenant(string name, int id)
        {
            Name = name;
            ID = id;
        }

        public DSRCovenant()
        {
            Name = "";
            ID = -1;
        }

        public static List<DSRCovenant> All = new List<DSRCovenant>();

        static DSRCovenant()
        {
            foreach (string line in Regex.Split(GetTxtResourceClass.GetTxtResource("Resources/Systems/Other/Covenants.txt"), "[\r\n]+"))
            {
                if (GetTxtResourceClass.IsValidTxtResource(line)) //determine if line is a valid resource or not
                {
                    Match match = covenantEntryRx.Match(line);
                    byte id = byte.Parse(match.Groups["ID"].Value);
                    string name = match.Groups["Name"].Value;
                    All.Add(new DSRCovenant(name, id));
                }
            }
            All.Sort();
        }
    }
}
