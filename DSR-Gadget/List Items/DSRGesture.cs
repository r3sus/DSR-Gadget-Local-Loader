using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DSR_Gadget
{
    class DSRGesture
    {
        private static Regex gestureEntryRx = new Regex(@"^(?<ID>\S+) (?<Name>.+)$");

        public string Name;
        public byte ID;

        private DSRGesture(string config)
        {
            Match gestureEntry = gestureEntryRx.Match(config);
            Name = gestureEntry.Groups["Name"].Value;
            ID = Convert.ToByte(gestureEntry.Groups["ID"].Value);
        }

        public override string ToString()
        {
            return Name;
        }

        public DSRGesture(string name, byte id)
        {
            Name = name;
            ID = id;
        }

        public static List<DSRGesture> All = new List<DSRGesture>();

        static DSRGesture()
        {
            foreach (string line in Regex.Split(Properties.Resources.Gestures, "[\r\n]+"))
                All.Add(new DSRGesture(line));
        }
    }
}
