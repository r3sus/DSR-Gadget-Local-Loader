using System;
using System.Globalization;
using PropertyHook;

namespace DSR_Gadget
{
    class DSRSummonSignSfx
    {
        public PHPointer SummonSignPtr { get; }


        public byte SummonType
        {
            get => SummonSignPtr.ReadByte((int)DSROffsets.SosSignManSign.SummonType);
            set => SummonSignPtr.WriteByte((int)DSROffsets.SosSignManSign.SummonType, value);
        }

        public string Name
        {
            get => SummonSignPtr.ReadString((int)DSROffsets.SosSignManSign.Name, System.Text.Encoding.Unicode, 32);
        }

        /*
        public int Hair
        {
            get => SummonSignPtr.ReadInt32((int)DSROffsets.FrpgNetSosDbItem.Hair);
        }

        public int RightWep1
        {
            get => SummonSignPtr.ReadInt32((int)DSROffsets.FrpgNetSosDbItem.RightWep1);
        }

        public int RightWep2
        {
            get => SummonSignPtr.ReadInt32((int)DSROffsets.FrpgNetSosDbItem.RightWep2);
        }

        public int LeftWep1
        {
            get => SummonSignPtr.ReadInt32((int)DSROffsets.FrpgNetSosDbItem.LeftWep1);
        }

        public int LeftWep2
        {
            get => SummonSignPtr.ReadInt32((int)DSROffsets.FrpgNetSosDbItem.LeftWep2);
        }

        public int ArmorHead
        {
            get => SummonSignPtr.ReadInt32((int)DSROffsets.FrpgNetSosDbItem.ArmorHead);
        }

        public int ArmorChest
        {
            get => SummonSignPtr.ReadInt32((int)DSROffsets.FrpgNetSosDbItem.ArmorChest);
        }

        public int ArmorHands
        {
            get => SummonSignPtr.ReadInt32((int)DSROffsets.FrpgNetSosDbItem.ArmorHands);
        }

        public int ArmorLegs
        {
            get => SummonSignPtr.ReadInt32((int)DSROffsets.FrpgNetSosDbItem.ArmorLegs);
        }

        public int Arrow1
        {
            get => SummonSignPtr.ReadInt32((int)DSROffsets.FrpgNetSosDbItem.Arrow1);
        }

        public int Arrow2
        {
            get => SummonSignPtr.ReadInt32((int)DSROffsets.FrpgNetSosDbItem.Arrow2);
        }

        public int Bolt1
        {
            get => SummonSignPtr.ReadInt32((int)DSROffsets.FrpgNetSosDbItem.Bolt1);
        }

        public int Bolt2
        {
            get => SummonSignPtr.ReadInt32((int)DSROffsets.FrpgNetSosDbItem.Bolt2);
        }*/

        public float PosX
        {
            get => SummonSignPtr.ReadSingle((int)DSROffsets.SosSignManSign.PosX);
            set => SummonSignPtr.WriteSingle((int)DSROffsets.SosSignManSign.PosX, value);
        }

        public float PosY
        {
            get => SummonSignPtr.ReadSingle((int)DSROffsets.SosSignManSign.PosY);
            set => SummonSignPtr.WriteSingle((int)DSROffsets.SosSignManSign.PosY, value);
        }

        public float PosZ
        {
            get => SummonSignPtr.ReadSingle((int)DSROffsets.SosSignManSign.PosZ);
            set => SummonSignPtr.WriteSingle((int)DSROffsets.SosSignManSign.PosZ, value);
        }

        public float PosAngle
        {
            get => SummonSignPtr.ReadSingle((int)DSROffsets.SosSignManSign.PosAngle);
            set => SummonSignPtr.WriteSingle((int)DSROffsets.SosSignManSign.PosAngle, value);
        }

        public DSRPlayer.Position GetPosition()
        {
            return new DSRPlayer.Position(PosX, PosY, PosZ, PosAngle);
        }



        public override string ToString()
        {
            DSRSummon summonType = DSRSummon.All.Find(p => p.ID == SummonType);
            return Name + " (" + summonType.Name + ")";
        }

        public DSRSummonSignSfx(PHPointer summonSignPtr, DSRHook dsrHook)
        {
            SummonSignPtr = summonSignPtr;
        }
    }
}
