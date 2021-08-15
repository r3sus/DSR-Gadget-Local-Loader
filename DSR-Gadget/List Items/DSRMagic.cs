using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DSR_Gadget
{
    class DSRMagic
    {
        private static Regex magicEntryRx = new Regex(@"^(?<ID>\S+) (?<Quantity>\S+) (?<Name>.+)$");

        public string Name;
        public int ID;
        public int Quantity;

        private DSRMagic(string config)
        {
            Match magicEntry = magicEntryRx.Match(config);
            Name = magicEntry.Groups["Name"].Value;
            ID = Convert.ToInt32(magicEntry.Groups["ID"].Value);
            Quantity = Convert.ToInt32(magicEntry.Groups["Quantity"].Value);
        }

        public override string ToString()
        {
            return Name;
        }

        public static List<DSRMagic> All = new List<DSRMagic>();
        public static Dictionary<int, int> Dictionary = new Dictionary<int, int>();

        static DSRMagic()
        {
            foreach (string line in Regex.Split(Properties.Resources.Magic, "[\r\n]+"))
            {
                DSRMagic magic = new DSRMagic(line);
                All.Add(magic);
                Dictionary[magic.ID] = magic.Quantity;
            }
        }
    }
}
