using DSR_Gadget.List_Items;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DSR_Gadget
{
    class DSRGesture : IDSRProperty
    {
        private static Regex gestureEntryRx = new Regex(@"^(?<ID>\S+) (?<Name>.+)$");

        public string Name { get; set; }
        public int ID { get; set; }

        private DSRGesture(string config)
        {
            Match gestureEntry = gestureEntryRx.Match(config);
            Name = gestureEntry.Groups["Name"].Value;
            ID = Convert.ToInt32(gestureEntry.Groups["ID"].Value);
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(IDSRProperty other)
        {
            return Name.CompareTo(other.Name);
        }

        public DSRGesture(string name, int id)
        {
            Name = name;
            ID = id;
        }

        public DSRGesture()
        {
            Name = "";
            ID = -1;
        }

        public static List<DSRGesture> All = new List<DSRGesture>();

        static DSRGesture()
        {
            foreach (string line in Regex.Split(Properties.Resources.Gestures, "[\r\n]+"))
                All.Add(new DSRGesture(line));
        }
    }
}
