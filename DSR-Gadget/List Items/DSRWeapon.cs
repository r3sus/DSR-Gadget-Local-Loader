using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DSR_Gadget
{
    class DSRWeapon : IDSRProperty
    {
        private static Regex weaponEntryRx = new Regex(@"^(?<ID>\S+) (?<Name>.+)$");

        public string Name { get; set; }
        public int ID { get; set;  } 

        private DSRWeapon(string config)
        {
            Match weaponEntry = weaponEntryRx.Match(config);
            Name = weaponEntry.Groups["Name"].Value;
            ID = Convert.ToInt32(weaponEntry.Groups["ID"].Value);
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(IDSRProperty other)
        {
            return Name.CompareTo(other.Name);
        }

        public DSRWeapon(string name, int id)
        {
            Name = name;
            ID = id;
        }

        public DSRWeapon()
        {
            Name = "";
            ID = -1;
        }

        //public static List<DSRWeapon> All = new List<DSRWeapon>();
        public static Dictionary<int, string> Dict = new Dictionary<int, string>();

        static DSRWeapon()
        {
            foreach (string line in Regex.Split(Properties.Resources.AllWeapons, "[\r\n]+"))
            {
                DSRWeapon weapon = new DSRWeapon(line);
                Dict.Add(weapon.ID, weapon.Name);
                //All.Add(new DSRWeapon(line));
            }
        }
    }
}
