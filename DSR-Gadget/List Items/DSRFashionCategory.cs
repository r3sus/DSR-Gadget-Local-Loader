using System;
using System.Collections.Generic;

namespace DSR_Gadget
{
    class DSRFashionCategory
    {
        public string Name;
        public int ID;
        public List<DSRItem> Items;

        private DSRFashionCategory(string name, int categoryID, string itemList, bool showIDs)
        {
            Name = name;
            ID = categoryID;
            Items = new List<DSRItem>();
            foreach (string line in GetTxtResourceClass.RegexSplit(itemList, "[\r\n]+"))
            {
                if (GetTxtResourceClass.IsValidTxtResource(line)) //determine if line is a valid resource or not
                    Items.Add(new DSRItem(line, showIDs, categoryID));
            };
            Items.Sort();
        }

        public override string ToString()
        {
            return Name;
        }

        public static void GetItemCategories()
        {
            foreach (string line in GetTxtResourceClass.RegexSplit(GetTxtResourceClass.GetTxtResource("Resources/Equipment/DSRFashionCategories.txt"), "[\r\n]+"))
            {
                if (GetTxtResourceClass.IsValidTxtResource(line)) //determine if line is a valid resource or not
                {
                    var att = GetTxtResourceClass.RegexSplit(line, ",");
                    Array.ForEach<string>(att, x => att[Array.IndexOf<string>(att, x)] = x.Trim());
                    All.Add(new DSRFashionCategory(att[0], Convert.ToInt32(att[1], 16), GetTxtResourceClass.GetTxtResource(att[2]), bool.Parse(att[3])));
                }
            };
        }

        public static List<DSRFashionCategory> All = new List<DSRFashionCategory>();
    }
}