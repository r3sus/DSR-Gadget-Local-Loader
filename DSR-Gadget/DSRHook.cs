using PropertyHook;
using System;
using System.Collections.Generic;

namespace DSR_Gadget
{
    internal class DSRHook : PHook
    {
        private DSROffsets Offsets;

        private PHPointer GroupMaskAddr;
        private PHPointer ChrDbgAddr;
        private PHPointer GameDataManBasePtr;
        private PHPointer ItemGetAddr;
        private PHPointer BonfireWarpAddr;

        private PHPointer CamMan;
        private PHPointer ChrClassWarp;
        private PHPointer ChrFollowCam;
        private PHPointer WorldChrBase;
        private PHPointer ChrData1;
        private PHPointer ChrMapData;
        private PHPointer ChrAnimData;
        private PHPointer ChrPosData;
        private PHPointer PlayerGameData;
        private PHPointer GraphicsData;
        private PHPointer MenuMan;
        private PHPointer EventFlags;

        public DSRHook(int refreshInterval, int minLifetime) :
            base(refreshInterval, minLifetime, p => p.MainWindowTitle == "DARK SOULS™: REMASTERED")
        {
            Offsets = new DSROffsets();
            CamMan = RegisterRelativeAOB(DSROffsets.CamManBaseAOB, 3, 7, DSROffsets.CamManOffset);
            ChrFollowCam = RegisterRelativeAOB(DSROffsets.ChrFollowCamAOB, 3, 7, DSROffsets.ChrFollowCamOffset1, DSROffsets.ChrFollowCamOffset2, DSROffsets.ChrFollowCamOffset3);
            GroupMaskAddr = RegisterRelativeAOB(DSROffsets.GroupMaskAOB, 2, 7);
            GraphicsData = RegisterRelativeAOB(DSROffsets.GraphicsDataAOB, 3, 7, DSROffsets.GraphicsDataOffset1, DSROffsets.GraphicsDataOffset2);
            ChrClassWarp = RegisterRelativeAOB(DSROffsets.ChrClassWarpAOB, 3, 7, DSROffsets.ChrClassWarpOffset1);
            WorldChrBase = RegisterRelativeAOB(DSROffsets.WorldChrBaseAOB, 3, 7, DSROffsets.WorldChrBaseOffset1);
            ChrDbgAddr = RegisterRelativeAOB(DSROffsets.ChrDbgAOB, 2, 7);
            MenuMan = RegisterRelativeAOB(DSROffsets.MenuManAOB, 3, 7, DSROffsets.MenuManOffset1);
            GameDataManBasePtr = RegisterRelativeAOB(DSROffsets.GameDataManAOB, 3, 7);
            EventFlags = RegisterRelativeAOB(DSROffsets.EventFlagsAOB, 3, 7, DSROffsets.EventFlagsOffset1, DSROffsets.EventFlagsOffset2);
            ItemGetAddr = RegisterAbsoluteAOB(DSROffsets.ItemGetAOB);
            BonfireWarpAddr = RegisterAbsoluteAOB(DSROffsets.BonfireWarpAOB);

            ChrData1 = CreateChildPointer(WorldChrBase, (int)DSROffsets.WorldChrBase.ChrData1);
            ChrMapData = CreateBasePointer(IntPtr.Zero);
            ChrAnimData = CreateBasePointer(IntPtr.Zero);
            ChrPosData = CreateBasePointer(IntPtr.Zero);
            PlayerGameData = CreateChildPointer(GameDataManBasePtr, DSROffsets.GameDataManOffset1, (int)DSROffsets.GameDataMan.PlayerGameData);

            OnHooked += DSRHook_OnHooked;
        }

        private void DSRHook_OnHooked(object sender, PHEventArgs e)
        {
            Offsets = DSROffsets.GetOffsets(Process.MainModule.ModuleMemorySize);
            ChrMapData = CreateChildPointer(ChrData1, (int)DSROffsets.ChrData1.ChrMapData + Offsets.ChrData1Boost1);
            ChrAnimData = CreateChildPointer(ChrMapData, (int)DSROffsets.ChrMapData.ChrAnimData);
            ChrPosData = CreateChildPointer(ChrMapData, (int)DSROffsets.ChrMapData.ChrPosData);
        }

        private static readonly Dictionary<int, string> VersionStrings = new Dictionary<int, string>
        {
            [0x4869400] = "1.01",
            [0x496BE00] = "1.01.1",
            [0x37CB400] = "1.01.2",
            [0x3817800] = "1.03",
        };

        public string Version
        {
            get
            {
                if (Hooked)
                {
                    int size = Process.MainModule.ModuleMemorySize;
                    if (VersionStrings.TryGetValue(size, out string version))
                        return version;
                    else
                        return $"0x{size:X8}";
                }
                else
                {
                    return "N/A";
                }
            }
        }

        public bool Loaded => ChrFollowCam.Resolve() != IntPtr.Zero;

        public bool Focused => Hooked && User32.GetForegroundProcessID() == Process.Id;

        #region Player
        public int Health
        {
            get => ChrData1.ReadInt32((int)DSROffsets.ChrData1.Health + Offsets.ChrData1Boost2);
            set => ChrData1.WriteInt32((int)DSROffsets.ChrData1.Health + Offsets.ChrData1Boost2, value);
        }

        public int HealthMax
        {
            get => ChrData1.ReadInt32((int)DSROffsets.ChrData1.MaxHealth + Offsets.ChrData1Boost2);
        }

        public int Stamina
        {
            get => ChrData1.ReadInt32((int)DSROffsets.ChrData1.Stamina + Offsets.ChrData1Boost2);
            set => ChrData1.WriteInt32((int)DSROffsets.ChrData1.Stamina + Offsets.ChrData1Boost2, value);
        }

        public int StaminaMax
        {
            get => ChrData1.ReadInt32((int)DSROffsets.ChrData1.MaxStamina + Offsets.ChrData1Boost2);
        }

        public int ChrType
        {
            get => ChrData1.ReadInt32((int)DSROffsets.ChrData1.ChrType + Offsets.ChrData1Boost2);
            set => ChrData1.WriteInt32((int)DSROffsets.ChrData1.ChrType + Offsets.ChrData1Boost2, value);
        }

        public int TeamType
        {
            get => ChrData1.ReadInt32((int)DSROffsets.ChrData1.TeamType + Offsets.ChrData1Boost2);
            set => ChrData1.WriteInt32((int)DSROffsets.ChrData1.TeamType + Offsets.ChrData1Boost2, value);
        }

        public byte InvadeType
        {
            get => PlayerGameData.ReadByte((int)DSROffsets.PlayerGameData.InvadeType);
            set => PlayerGameData.WriteByte((int)DSROffsets.PlayerGameData.InvadeType, value);
        }

        public int MPAreaID
        {
            get => ChrData1.ReadInt32((int)DSROffsets.ChrData1.MPAreaID + Offsets.ChrData1Boost2);
            set => ChrData1.WriteInt32((int)DSROffsets.ChrData1.MPAreaID + Offsets.ChrData1Boost2, value);
        }

        public int AreaID
        {
            get => ChrData1.ReadInt32((int)DSROffsets.ChrData1.AreaID + Offsets.ChrData1Boost2);
            set => ChrData1.WriteInt32((int)DSROffsets.ChrData1.AreaID + Offsets.ChrData1Boost2, value);
        }

        public void GetPosition(out float x, out float y, out float z, out float angle)
        {
            x = ChrPosData.ReadSingle((int)DSROffsets.ChrPosData.PosX);
            y = ChrPosData.ReadSingle((int)DSROffsets.ChrPosData.PosY);
            z = ChrPosData.ReadSingle((int)DSROffsets.ChrPosData.PosZ);
            angle = ChrPosData.ReadSingle((int)DSROffsets.ChrPosData.PosAngle);
        }

        public void GetStablePosition(out float x, out float y, out float z, out float angle)
        {
            x = ChrClassWarp.ReadSingle((int)DSROffsets.ChrClassWarp.StableX + Offsets.ChrClassWarpBoost);
            y = ChrClassWarp.ReadSingle((int)DSROffsets.ChrClassWarp.StableY + Offsets.ChrClassWarpBoost);
            z = ChrClassWarp.ReadSingle((int)DSROffsets.ChrClassWarp.StableZ + Offsets.ChrClassWarpBoost);
            angle = ChrClassWarp.ReadSingle((int)DSROffsets.ChrClassWarp.StableAngle + Offsets.ChrClassWarpBoost);
        }

        public void PosWarp(float x, float y, float z, float angle)
        {
            ChrMapData.WriteSingle((int)DSROffsets.ChrMapData.WarpX, x);
            ChrMapData.WriteSingle((int)DSROffsets.ChrMapData.WarpY, y);
            ChrMapData.WriteSingle((int)DSROffsets.ChrMapData.WarpZ, z);
            ChrMapData.WriteSingle((int)DSROffsets.ChrMapData.WarpAngle, angle);
            ChrMapData.WriteBoolean((int)DSROffsets.ChrMapData.Warp, true);
        }

        public bool NoGravity
        {
            set => ChrData1.WriteFlag32((int)DSROffsets.ChrData1.ChrFlags1 + Offsets.ChrData1Boost1, (uint)DSROffsets.ChrFlags1.NoGravity, value);
        }

        public bool NoCollision
        {
            set => ChrMapData.WriteFlag32((int)DSROffsets.ChrMapData.ChrMapFlags, (uint)DSROffsets.ChrMapFlags.DisableMapHit, value);
        }

        public bool DeathCam
        {
            get => WorldChrBase.ReadBoolean((int)DSROffsets.WorldChrBase.DeathCam);
            set => WorldChrBase.WriteBoolean((int)DSROffsets.WorldChrBase.DeathCam, value);
        }

        public int LastBonfire
        {
            get => ChrClassWarp.ReadInt32((int)DSROffsets.ChrClassWarp.LastBonfire + Offsets.ChrClassWarpBoost);
            set => ChrClassWarp.WriteInt32((int)DSROffsets.ChrClassWarp.LastBonfire + Offsets.ChrClassWarpBoost, value);
        }

        public void BonfireWarp()
        {
            byte[] asm = (byte[])DSRAssembly.BonfireWarp.Clone();
            byte[] bytes = BitConverter.GetBytes(GameDataManBasePtr.Resolve().ToInt64());
            Array.Copy(bytes, 0, asm, 0x2, 8);
            bytes = BitConverter.GetBytes(BonfireWarpAddr.Resolve().ToInt64());
            Array.Copy(bytes, 0, asm, 0x18, 8);
            Execute(asm);
        }

        public float AnimSpeed
        {
            set => ChrAnimData.WriteSingle((int)DSROffsets.ChrAnimData.AnimSpeed, value);
        }

        public byte[] DumpFollowCam()
        {
            return ChrFollowCam.ReadBytes(0, 512);
        }

        public void UndumpFollowCam(byte[] value)
        {
            ChrFollowCam.WriteBytes(0, value);
        }
        #endregion

        #region Stats
        public byte Class
        {
            get => PlayerGameData.ReadByte((int)DSROffsets.PlayerGameData.Class);
            set => PlayerGameData.WriteByte((int)DSROffsets.PlayerGameData.Class, value);
        }

        public int Humanity
        {
            get => PlayerGameData.ReadInt32((int)DSROffsets.PlayerGameData.Humanity);
            set => PlayerGameData.WriteInt32((int)DSROffsets.PlayerGameData.Humanity, value);
        }

        public int Souls
        {
            get => PlayerGameData.ReadInt32((int)DSROffsets.PlayerGameData.Souls);
            set => PlayerGameData.WriteInt32((int)DSROffsets.PlayerGameData.Souls, value);
        }

        public int SoulLevel
        {
            get => PlayerGameData.ReadInt32((int)DSROffsets.PlayerGameData.SoulLevel);
        }

        public int Vitality
        {
            get => PlayerGameData.ReadInt32((int)DSROffsets.PlayerGameData.Vitality);
        }

        public int Attunement
        {
            get => PlayerGameData.ReadInt32((int)DSROffsets.PlayerGameData.Attunement);
        }

        public int Endurance
        {
            get => PlayerGameData.ReadInt32((int)DSROffsets.PlayerGameData.Endurance);
        }

        public int Strength
        {
            get => PlayerGameData.ReadInt32((int)DSROffsets.PlayerGameData.Strength);
        }

        public int Dexterity
        {
            get => PlayerGameData.ReadInt32((int)DSROffsets.PlayerGameData.Dexterity);
        }

        public int Resistance
        {
            get => PlayerGameData.ReadInt32((int)DSROffsets.PlayerGameData.Resistance);
        }

        public int Intelligence
        {
            get => PlayerGameData.ReadInt32((int)DSROffsets.PlayerGameData.Intelligence);
        }

        public int Faith
        {
            get => PlayerGameData.ReadInt32((int)DSROffsets.PlayerGameData.Faith);
        }
        #endregion

        #region Items
        public void GetItem(int category, int id, int quantity)
        {
            byte[] asm = (byte[])DSRAssembly.GetItem.Clone();

            byte[] bytes = BitConverter.GetBytes(category);
            Array.Copy(bytes, 0, asm, 0x1, 4);
            bytes = BitConverter.GetBytes(quantity);
            Array.Copy(bytes, 0, asm, 0x7, 4);
            bytes = BitConverter.GetBytes(id);
            Array.Copy(bytes, 0, asm, 0xD, 4);
            bytes = BitConverter.GetBytes((ulong)GameDataManBasePtr.Resolve());
            Array.Copy(bytes, 0, asm, 0x19, 8);
            bytes = BitConverter.GetBytes((ulong)ItemGetAddr.Resolve());
            Array.Copy(bytes, 0, asm, 0x46, 8);

            Execute(asm);
        }
        #endregion

        #region Cheats
        public bool PlayerDeadMode
        {
            get => ChrData1.ReadFlag32((int)DSROffsets.ChrData1.ChrFlags1 + Offsets.ChrData1Boost1, (uint)DSROffsets.ChrFlags1.SetDeadMode);
            set => ChrData1.WriteFlag32((int)DSROffsets.ChrData1.ChrFlags1 + Offsets.ChrData1Boost1, (uint)DSROffsets.ChrFlags1.SetDeadMode, value);
        }

        public bool PlayerNoDead
        {
            set => ChrDbgAddr.WriteBoolean((int)DSROffsets.ChrDbg.PlayerNoDead, value);
        }

        public bool PlayerDisableDamage
        {
            set => ChrData1.WriteFlag32((int)DSROffsets.ChrData1.ChrFlags1 + Offsets.ChrData1Boost1, (uint)DSROffsets.ChrFlags1.DisableDamage, value);
        }

        public bool PlayerNoHit
        {
            set => ChrData1.WriteFlag32((int)DSROffsets.ChrData1.ChrFlags2 + Offsets.ChrData1Boost2, (uint)DSROffsets.ChrFlags2.NoHit, value);
        }

        public bool PlayerNoStamina
        {
            set => ChrData1.WriteFlag32((int)DSROffsets.ChrData1.ChrFlags2 + Offsets.ChrData1Boost2, (uint)DSROffsets.ChrFlags2.NoStaminaConsumption, value);
        }

        public bool PlayerSuperArmor
        {
            set => ChrData1.WriteFlag32((int)DSROffsets.ChrData1.ChrFlags1 + Offsets.ChrData1Boost1, (uint)DSROffsets.ChrFlags1.SetSuperArmor, value);
        }

        public bool PlayerHide
        {
            set => ChrDbgAddr.WriteBoolean((int)DSROffsets.ChrDbg.PlayerHide, value);
        }

        public bool PlayerSilence
        {
            set => ChrDbgAddr.WriteBoolean((int)DSROffsets.ChrDbg.PlayerSilence, value);
        }

        public bool PlayerExterminate
        {
            set => ChrDbgAddr.WriteBoolean((int)DSROffsets.ChrDbg.PlayerExterminate, value);
        }

        public bool PlayerNoGoods
        {
            set => ChrData1.WriteFlag32((int)DSROffsets.ChrData1.ChrFlags2 + Offsets.ChrData1Boost2, (uint)DSROffsets.ChrFlags2.NoGoodsConsume, value);
        }

        public bool AllNoArrow
        {
            set => ChrDbgAddr.WriteBoolean((int)DSROffsets.ChrDbg.AllNoArrowConsume, value);
        }

        public bool AllNoMagicQty
        {
            set => ChrDbgAddr.WriteBoolean((int)DSROffsets.ChrDbg.AllNoMagicQtyConsume, value);
        }

        public bool AllNoDead
        {
            set => ChrDbgAddr.WriteBoolean((int)DSROffsets.ChrDbg.AllNoDead, value);
        }

        public bool AllNoDamage
        {
            set => ChrDbgAddr.WriteBoolean((int)DSROffsets.ChrDbg.AllNoDamage, value);
        }

        public bool AllNoHit
        {
            set => ChrDbgAddr.WriteBoolean((int)DSROffsets.ChrDbg.AllNoHit, value);
        }

        public bool AllNoStamina
        {
            set => ChrDbgAddr.WriteBoolean((int)DSROffsets.ChrDbg.AllNoStaminaConsume, value);
        }

        public bool AllNoAttack
        {
            set => ChrDbgAddr.WriteBoolean((int)DSROffsets.ChrDbg.AllNoAttack, value);
        }

        public bool AllNoMove
        {
            set => ChrDbgAddr.WriteBoolean((int)DSROffsets.ChrDbg.AllNoMove, value);
        }

        public bool AllNoUpdateAI
        {
            set => ChrDbgAddr.WriteBoolean((int)DSROffsets.ChrDbg.AllNoUpdateAI, value);
        }
        #endregion

        #region Graphics
        public bool DrawMap
        {
            set => GroupMaskAddr.WriteBoolean((int)DSROffsets.GroupMask.Map, value);
        }

        public bool DrawObjects
        {
            set => GroupMaskAddr.WriteBoolean((int)DSROffsets.GroupMask.Objects, value);
        }

        public bool DrawCharacters
        {
            set => GroupMaskAddr.WriteBoolean((int)DSROffsets.GroupMask.Characters, value);
        }

        public bool DrawSFX
        {
            set => GroupMaskAddr.WriteBoolean((int)DSROffsets.GroupMask.SFX, value);
        }

        public bool DrawCutscenes
        {
            set => GroupMaskAddr.WriteBoolean((int)DSROffsets.GroupMask.Cutscenes, value);
        }

        public bool FilterOverride
        {
            set => GraphicsData.WriteBoolean((int)DSROffsets.GraphicsData.FilterOverride, value);
        }

        public void SetFilterValues(float brightR, float brightG, float brightB, float contR, float contG, float contB, float saturation, float hue)
        {
            GraphicsData.WriteSingle((int)DSROffsets.GraphicsData.FilterBrightnessR, brightR);
            GraphicsData.WriteSingle((int)DSROffsets.GraphicsData.FilterBrightnessG, brightG);
            GraphicsData.WriteSingle((int)DSROffsets.GraphicsData.FilterBrightnessB, brightB);
            GraphicsData.WriteSingle((int)DSROffsets.GraphicsData.FilterContrastR, contR);
            GraphicsData.WriteSingle((int)DSROffsets.GraphicsData.FilterContrastG, contG);
            GraphicsData.WriteSingle((int)DSROffsets.GraphicsData.FilterContrastB, contB);
            GraphicsData.WriteSingle((int)DSROffsets.GraphicsData.FilterSaturation, saturation);
            GraphicsData.WriteSingle((int)DSROffsets.GraphicsData.FilterHue, hue);
        }
        #endregion

        #region Misc
        private static Dictionary<string, int> eventFlagGroups = new Dictionary<string, int>()
        {
            {"0", 0x00000},
            {"1", 0x00500},
            {"5", 0x05F00},
            {"6", 0x0B900},
            {"7", 0x11300},
        };

        private static Dictionary<string, int> eventFlagAreas = new Dictionary<string, int>()
        {
            {"000", 00},
            {"100", 01},
            {"101", 02},
            {"102", 03},
            {"110", 04},
            {"120", 05},
            {"121", 06},
            {"130", 07},
            {"131", 08},
            {"132", 09},
            {"140", 10},
            {"141", 11},
            {"150", 12},
            {"151", 13},
            {"160", 14},
            {"170", 15},
            {"180", 16},
            {"181", 17},
        };

        private int getEventFlagOffset(int ID, out uint mask)
        {
            string idString = ID.ToString("D8");
            if (idString.Length == 8)
            {
                string group = idString.Substring(0, 1);
                string area = idString.Substring(1, 3);
                int section = Int32.Parse(idString.Substring(4, 1));
                int number = Int32.Parse(idString.Substring(5, 3));

                if (eventFlagGroups.ContainsKey(group) && eventFlagAreas.ContainsKey(area))
                {
                    int offset = eventFlagGroups[group];
                    offset += eventFlagAreas[area] * 0x500;
                    offset += section * 128;
                    offset += (number - (number % 32)) / 8;

                    mask = 0x80000000 >> (number % 32);
                    return offset;
                }
            }
            throw new ArgumentException("Unknown event flag ID: " + ID);
        }

        public bool ReadEventFlag(int ID)
        {
            int offset = getEventFlagOffset(ID, out uint mask);
            return EventFlags.ReadFlag32(offset, mask);
        }

        public void WriteEventFlag(int ID, bool state)
        {
            int offset = getEventFlagOffset(ID, out uint mask);
            EventFlags.WriteFlag32(offset, mask, state);
        }
        #endregion

        #region Hotkeys
        public void MenuKick()
        {
            MenuMan.WriteInt32((int)DSROffsets.MenuMan.MenuKick, 2);
        }

#if DEBUG
        public void HotkeyTest1()
        {

        }

        public void HotkeyTest2()
        {

        }
#endif
        #endregion
    }
}
