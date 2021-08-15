using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DSR_Gadget
{
    class DSRPlayer
    {
        public string Name      = "";
        public int PlayerIndex  = -1;

        public int SoulLevel    = -1;
        public int Vitality     = -1;
        public int Attunement   = -1;
        public int Endurance    = -1;
        public int Strength     = -1;
        public int Dexterity    = -1;
        public int Resistance   = -1;
        public int Intelligence = -1;
        public int Faith        = -1;
        public int Humanity     = -1;

        public int LeftWep1     = -1;
        public int RightWep1    = -1;
        public int LeftWep2     = -1;
        public int RightWep2    = -1;
        public int Arrow1       = -1;
        public int Bolt1        = -1;
        public int Arrow2       = -1;
        public int Bolt2        = -1;
        public int ArmorHead    = -1;
        public int ArmorChest   = -1;
        public int ArmorHands   = -1;
        public int ArmorLegs    = -1;
        public int Hair         = -1;
        public int Ring1        = -1;
        public int Ring2        = -1;
        public int QuickBar1    = -1;
        public int QuickBar2    = -1;
        public int QuickBar3    = -1;
        public int QuickBar4    = -1;
        public int QuickBar5    = -1;

        public int ChrType      = -1;
        public int TeamType     = -1;

        public override string ToString()
        {
            return PlayerIndex + ": " + Name;
        }

        public DSRPlayer(int playerIndex)
        {
            PlayerIndex = playerIndex;
        }

        public DSRPlayer(string name, int chrType, int teamType)
        {
            Name = name;
            ChrType = chrType;
            TeamType = teamType;
        }
    }
}
