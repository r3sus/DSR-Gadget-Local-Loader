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
            foreach (string line in Regex.Split(Properties.Resources.Genders, "[\r\n]+"))
                All.Add(new DSRGender(line));
        }
    }
}
