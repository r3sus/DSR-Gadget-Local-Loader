using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DSR_Gadget
{
    class DSRCovenant
    {
        private static Regex covenantEntryRx = new Regex(@"^(?<ID>\S+) (?<Name>.+)$");

        public string Name;
        public byte ID;

        private DSRCovenant(string config)
        {
            Match covenantEntry = covenantEntryRx.Match(config);
            Name = covenantEntry.Groups["Name"].Value;
            ID = Convert.ToByte(covenantEntry.Groups["ID"].Value);
        }

        public override string ToString()
        {
            return Name;
        }

        public DSRCovenant(string name, byte id)
        {
            Name = name;
            ID = id;
        }

        public static List<DSRCovenant> All = new List<DSRCovenant>();

        static DSRCovenant()
        {
            foreach (string line in Regex.Split(Properties.Resources.Covenants, "[\r\n]+"))
                All.Add(new DSRCovenant(line));
        }
    }
}
