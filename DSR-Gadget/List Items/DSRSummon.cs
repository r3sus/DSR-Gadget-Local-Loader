using DSR_Gadget.List_Items;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DSR_Gadget
{
    class DSRSummon : IDSRProperty
    {
        private static Regex summonEntryRx = new Regex(@"^(?<ID>\S+) (?<Name>.+)$");

        public string Name { get; set; }
        public int ID { get; set; }

        private DSRSummon(string config)
        {
            Match summonEntry = summonEntryRx.Match(config);
            Name = summonEntry.Groups["Name"].Value;
            ID = Convert.ToInt32(summonEntry.Groups["ID"].Value);
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(IDSRProperty other)
        {
            return Name.CompareTo(other.Name);
        }

        public DSRSummon(string name, int id)
        {
            Name = name;
            ID = id;
        }

        public DSRSummon()
        {
            Name = "";
            ID = -1;
        }

        public static List<DSRSummon> All = new List<DSRSummon>();

        static DSRSummon()
        {
            foreach (string line in Regex.Split(Properties.Resources.Summons, "[\r\n]+"))
                All.Add(new DSRSummon(line));
        }
    }
}
