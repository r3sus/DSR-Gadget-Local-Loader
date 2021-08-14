using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DSR_Gadget
{
    class DSRTeam
    {
        private static Regex teamEntryRx = new Regex(@"^(?<ChrType>\S+) (?<TeamType>\S+) (?<Name>.+)$");

        public string Name;
        public int ChrType;
        public int TeamType;

        private DSRTeam(string config)
        {
            Match teamEntry = teamEntryRx.Match(config);
            Name = teamEntry.Groups["Name"].Value;
            ChrType = Convert.ToInt32(teamEntry.Groups["ChrType"].Value);
            TeamType = Convert.ToInt32(teamEntry.Groups["TeamType"].Value);
        }

        public override string ToString()
        {
            return Name;
        }

        public DSRTeam(string name, int chrType, int teamType)
        {
            Name = name;
            ChrType = chrType;
            TeamType = teamType;
        }

        public static List<DSRTeam> All = new List<DSRTeam>();

        static DSRTeam()
        {
            foreach (string line in Regex.Split(Properties.Resources.Teams, "[\r\n]+"))
                All.Add(new DSRTeam(line));
        }
    }
}
