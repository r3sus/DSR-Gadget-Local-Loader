using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DSR_Gadget
{
    class DSRInvasion : IDSRProperty
    {
        private static Regex invasionEntryRx = new Regex(@"^(?<ID>\S+) (?<Name>.+)$");

        public string Name { get; set; }
        public int ID { get; set; }

        private DSRInvasion(string config)
        {
            Match invasionEntry = invasionEntryRx.Match(config);
            Name = invasionEntry.Groups["Name"].Value;
            ID = Convert.ToInt32(invasionEntry.Groups["ID"].Value);
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(IDSRProperty other)
        {
            return Name.CompareTo(other.Name);
        }

        public DSRInvasion(string name, int id)
        {
            Name = name;
            ID = id;
        }

        public DSRInvasion()
        {
            Name = "";
            ID = -1;
        }

        public static List<DSRInvasion> All = new List<DSRInvasion>();

        static DSRInvasion()
        {
            foreach (string line in Regex.Split(Properties.Resources.Invasions, "[\r\n]+"))
                All.Add(new DSRInvasion(line));
        }
    }
}
