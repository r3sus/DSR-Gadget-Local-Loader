using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DSR_Gadget
{
    class DSRInvasion
    {
        private static Regex invasionEntryRx = new Regex(@"^(?<InvadeType>\S+) (?<Name>.+)$");

        public string Name;
        public byte InvadeType;

        private DSRInvasion(string config)
        {
            Match invasionEntry = invasionEntryRx.Match(config);
            Name = invasionEntry.Groups["Name"].Value;
            InvadeType = Convert.ToByte(invasionEntry.Groups["InvadeType"].Value);
        }

        public override string ToString()
        {
            return Name;
        }

        public DSRInvasion(string name, byte invadeType)
        {
            Name = name;
            InvadeType = invadeType;
        }

        public static List<DSRInvasion> All = new List<DSRInvasion>();

        static DSRInvasion()
        {
            foreach (string line in Regex.Split(Properties.Resources.Invasions, "[\r\n]+"))
                All.Add(new DSRInvasion(line));
        }
    }
}
