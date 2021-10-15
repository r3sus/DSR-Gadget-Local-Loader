using DSR_Gadget.List_Items;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DSR_Gadget
{
    class DSRGender : IDSRProperty
    {
        private static Regex genderEntryRx = new Regex(@"^(?<ID>\S+) (?<Name>.+)$");

        public string Name { get; set; }
        public int ID{ get; set; }

        private DSRGender(string config)
        {
            Match genderEntry = genderEntryRx.Match(config);
            Name = genderEntry.Groups["Name"].Value;
            ID = Convert.ToInt32(genderEntry.Groups["ID"].Value);
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(IDSRProperty other)
        {
            return Name.CompareTo(other.Name);
        }

        public DSRGender(string name, int id)
        {
            Name = name;
            ID = id;
        }

        public DSRGender()
        {
            Name = "";
            ID = -1;
        }

        public static List<DSRGender> All = new List<DSRGender>();

        static DSRGender()
        {
            foreach (string line in Regex.Split(GetTxtResourceClass.GetTxtResource("Resources/Systems/Other/Genders.txt"), "[\r\n]+"))
            {
                if (GetTxtResourceClass.IsValidTxtResource(line)) //determine if line is a valid resource or not
                {
                    Match match = genderEntryRx.Match(line);
                    byte id = byte.Parse(match.Groups["ID"].Value);
                    string name = match.Groups["Name"].Value;
                    All.Add(new DSRGender(name, id));
                }
            }
            All.Sort();
        }
    }
}
