using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DSR_Gadget
{
    class DSRArea
    {
        private static Regex areaEntryRx = new Regex(@"^(?<AreaID>\S+) (?<Name>.+)$");

        public string Name;
        public int AreaID;

        private DSRArea(string config)
        {
            Match areaEntry = areaEntryRx.Match(config);
            Name = areaEntry.Groups["Name"].Value;
            AreaID = Convert.ToInt32(areaEntry.Groups["AreaID"].Value);
        }

        public override string ToString()
        {
            return Name;
        }

        public DSRArea(string name, int areaID)
        {
            Name = name;
            AreaID = areaID;
        }

        public static List<DSRArea> All = new List<DSRArea>();

        static DSRArea()
        {
            foreach (string line in Regex.Split(Properties.Resources.Areas, "[\r\n]+"))
                All.Add(new DSRArea(line));
        }
    }
}
