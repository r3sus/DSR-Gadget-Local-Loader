using System;
using System.Globalization;
using PropertyHook;

namespace DSR_Gadget
{
    class DSRSummonSign
    {
        private PHPointer SummonSignPtr;


        public int SummonType
        {
            get => SummonSignPtr.ReadInt32((int)DSROffsets.FrpgNetSosDbItem.SummonType);
        }

        public int SoulLevel
        {
            get => SummonSignPtr.ReadInt32((int)DSROffsets.FrpgNetSosDbItem.SoulLevel);
        }

        public string Name
        {
            get => SummonSignPtr.ReadString((int)DSROffsets.FrpgNetSosDbItem.Name, System.Text.Encoding.Unicode, 32);
        }

        public string SteamID64Hex
        {
            get => SummonSignPtr.ReadString((int)DSROffsets.FrpgNetSosDbItem.SteamID, System.Text.Encoding.ASCII, 16);
        }

        public long SteamID64
        {
            get
            {
                long steamID64;
                long.TryParse(SteamID64Hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out steamID64);
                return steamID64;
            }
        }



        public override string ToString()
        {
            return Name + " (" + SteamID64.ToString() + ")";
        }

        public DSRSummonSign(PHPointer summonSignPtr, DSRHook dsrHook)
        {
            SummonSignPtr = summonSignPtr;
        }
    }
}
