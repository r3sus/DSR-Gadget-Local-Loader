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
        private PHPointer PlayerGameDataPtr;
        private PHPointer PlayerGameDataRecentPtr;
        private PHPointer[] RecentPlayerPtrs;
        private PHPointer PlayerGameDataCurrentPtr;
        private PHPointer[] CurrentPlayerPtrs;
        private PHPointer EquipMagicDataPtr;
        private PHPointer LastBloodstainPos;
        private PHPointer GraphicsData;
        private PHPointer MenuMan;
        private PHPointer EventFlags;
        private PHPointer ActionCtrlPtr;

        private PHPointer DurabilityAddr;
        private PHPointer DurabilitySpecialAddr;

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
            PlayerGameDataPtr = CreateChildPointer(GameDataManBasePtr, DSROffsets.GameDataManOffset1, (int)DSROffsets.GameDataMan.PlayerGameData);
            EquipMagicDataPtr = CreateChildPointer(GameDataManBasePtr, DSROffsets.GameDataManOffset1, (int)DSROffsets.GameDataMan.PlayerGameData, (int)DSROffsets.PlayerGameData.EquipMagicData);
            LastBloodstainPos = CreateChildPointer(GameDataManBasePtr, DSROffsets.GameDataManOffset1, (int)DSROffsets.GameDataMan.LastBloodstainPos);
            ActionCtrlPtr = CreateChildPointer(ChrData1, (int)DSROffsets.ChrData1.PlayerCtrl, (int)DSROffsets.PlayerCtrl.ActionCtrl);

            PlayerGameDataRecentPtr = CreateChildPointer(GameDataManBasePtr, DSROffsets.GameDataManOffset1, (int)DSROffsets.GameDataMan.PlayerGameDataRecent);
            RecentPlayerPtrs = new PHPointer[5];
            for (int i = 0; i < 5; i++)
            {
                RecentPlayerPtrs[i] = CreateChildPointer(PlayerGameDataRecentPtr, (int)DSROffsets.PlayerGameDataRecent.RecentPlayer1 + i * (int)DSROffsets.RecentPlayerOffset);
            }

            PlayerGameDataCurrentPtr = CreateChildPointer(ChrData1, (int)DSROffsets.ChrData1.CurrentPlayers);
            CurrentPlayerPtrs = new PHPointer[5];
            for (int i = 0; i < 5; i++)
            {
                CurrentPlayerPtrs[i] = CreateChildPointer(PlayerGameDataCurrentPtr, (int)DSROffsets.CurrentPlayers.CurrentPlayer1 + i * (int)DSROffsets.CurrentPlayerOffset, (int)DSROffsets.CurrentPlayersOffset);
            }

            DurabilityAddr = RegisterAbsoluteAOB(DSROffsets.DurabilityAOB);
            DurabilitySpecialAddr = RegisterAbsoluteAOB(DSROffsets.DurabilitySpecialAOB);

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

        public int[][] EquipMagicData
        {
            get
            {
                int[] magicData = new int[12];
                int[] magicQuantity = new int[12];
                for (int i = 0; i < 12; i++)
                {
                    magicData[i] = EquipMagicDataPtr.ReadInt32((int)DSROffsets.EquipMagicData.AttunementSlot1 + i * 8);
                    magicQuantity[i] = EquipMagicDataPtr.ReadInt32((int)DSROffsets.EquipMagicData.Quantity1 + i * 8);
                }

                int[][] equipMagicData = new int[2][];
                equipMagicData[0] = magicData;
                equipMagicData[1] = magicQuantity;
                return equipMagicData;
            }

            set
            {
                for (int i = 0; i < 12; i++)
                {
                    int id = EquipMagicDataPtr.ReadInt32((int)DSROffsets.EquipMagicData.AttunementSlot1 + i * 8);
                    if (id == value[0][i])
                    {
                        EquipMagicDataPtr.WriteInt32((int)DSROffsets.EquipMagicData.Quantity1 + i * 8, value[1][i]);
                    }
                    /*
                    EquipMagicDataPtr.WriteInt32((int)DSROffsets.EquipMagicData.AttunementSlot1 + i * 8, value[0][i]);
                    */
                }
            }
        }

        public void ResetMagicQuantity()
        {
            for (int i = 0; i < 12; i++)
            {
                int id = EquipMagicDataPtr.ReadInt32((int)DSROffsets.EquipMagicData.AttunementSlot1 + i * 8);
                EquipMagicDataPtr.WriteInt32((int)DSROffsets.EquipMagicData.Quantity1 + i * 8, DSRMagic.Dictionary[id]);
            }
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
            get => PlayerGameDataPtr.ReadByte((int)DSROffsets.PlayerGameData.InvadeType);
            set => PlayerGameDataPtr.WriteByte((int)DSROffsets.PlayerGameData.InvadeType, value);
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

        public int CurrentAnimation
        {
            get => ActionCtrlPtr.ReadInt32((int)DSROffsets.ActionCtrl.CurrentAnimation);
            set => ActionCtrlPtr.WriteInt32((int)DSROffsets.ActionCtrl.CurrentAnimation, value);
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

        public void GetInitialPosition(out float x, out float y, out float z, out float angle)
        {
            x = ChrClassWarp.ReadSingle((int)DSROffsets.ChrClassWarp.InitialX + Offsets.ChrClassWarpBoost);
            y = ChrClassWarp.ReadSingle((int)DSROffsets.ChrClassWarp.InitialY + Offsets.ChrClassWarpBoost);
            z = ChrClassWarp.ReadSingle((int)DSROffsets.ChrClassWarp.InitialZ + Offsets.ChrClassWarpBoost);
            angle = ChrClassWarp.ReadSingle((int)DSROffsets.ChrClassWarp.InitialAngle + Offsets.ChrClassWarpBoost);
        }

        public void GetLastBloodstainPosition(out float x, out float y, out float z, out float angle)
        {
            x = LastBloodstainPos.ReadSingle((int)DSROffsets.LastBloodstainPos.PosX);
            y = LastBloodstainPos.ReadSingle((int)DSROffsets.LastBloodstainPos.PosY);
            z = LastBloodstainPos.ReadSingle((int)DSROffsets.LastBloodstainPos.PosZ);
            angle = LastBloodstainPos.ReadSingle((int)DSROffsets.LastBloodstainPos.PosAngle);
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
            get => PlayerGameDataPtr.ReadByte((int)DSROffsets.PlayerGameData.Class);
            set => PlayerGameDataPtr.WriteByte((int)DSROffsets.PlayerGameData.Class, value);
        }

        public int Humanity
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.Humanity);
            set => PlayerGameDataPtr.WriteInt32((int)DSROffsets.PlayerGameData.Humanity, value);
        }

        public int Souls
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.Souls);
            set => PlayerGameDataPtr.WriteInt32((int)DSROffsets.PlayerGameData.Souls, value);
        }

        public int SoulLevel
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.SoulLevel);
        }

        public int Vitality
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.Vitality);
        }

        public int Attunement
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.Attunement);
        }

        public int Endurance
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.Endurance);
        }

        public int Strength
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.Strength);
        }

        public int Dexterity
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.Dexterity);
        }

        public int Resistance
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.Resistance);
        }

        public int Intelligence
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.Intelligence);
        }

        public int Faith
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.Faith);
        }

        public byte Covenant
        {
            get => PlayerGameDataPtr.ReadByte((int)DSROffsets.PlayerGameData.CurrentCovenant);
            set => PlayerGameDataPtr.WriteByte((int)DSROffsets.PlayerGameData.CurrentCovenant, value);
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

        private byte[] DurabilityOnBytes = { 0x41, 0x8D, 0x01, 0x90, 0x90, 0x90, 0x90};
        private byte[] DurabilityOffBytes = { 0x41, 0x8D, 0x41, 0xFF, 0x45, 0x8B, 0xC3 };

        public bool Durability
        {
            set
            {
                if (value)
                {
                    DurabilityAddr.WriteBytes(0x0, DurabilityOnBytes);
                }
                else
                {
                    DurabilityAddr.WriteBytes(0x0, DurabilityOffBytes);
                }

            }

            get
            {
                return DurabilityAddr.ReadBytes(0, 7).Equals(DurabilityOnBytes);
            }
        }

        private byte[] DurabilitySpecialOnBytes = { 0x49, 0x03, 0x49, 0x38, 0x90, 0x90, 0x90, 0x90};
        private byte[] DurabilitySpecialOffBytes = { 0x49, 0x03, 0x49, 0x38, 0x44, 0x89, 0x41, 0x14 };
        public bool DurabilitySpecial
        {
            set
            {
                if (value)
                {
                    DurabilitySpecialAddr.WriteBytes(0x0, DurabilitySpecialOnBytes);
                }
                else
                {
                    DurabilitySpecialAddr.WriteBytes(0x0, DurabilitySpecialOffBytes);
                }

            }

            get
            {
                return DurabilitySpecialAddr.ReadBytes(0, 8).Equals(DurabilitySpecialOnBytes);
            }
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

        #region Net

        public bool[] GetRecentPlayers()
        {
            bool[] recentPlayers = new bool[5];
            for (int i = 0; i < RecentPlayerPtrs.Length; i++)
            {
                //TODO: more robust check
                if (RecentPlayerPtrs[i].ReadInt32((int)DSROffsets.PlayerGameData.SoulLevel) != 0)
                    recentPlayers[i] = true;
                else
                    recentPlayers[i] = false;
            }

            return recentPlayers;
        }

        public bool[] GetCurrentPlayers()
        {
            bool[] currentPlayers = new bool[5];
            for (int i = 0; i < RecentPlayerPtrs.Length; i++)
            {
                //TODO: more robust check
                if (CurrentPlayerPtrs[i].ReadInt32((int)DSROffsets.PlayerGameData.SoulLevel) != 0)
                    currentPlayers[i] = true;
                else
                    currentPlayers[i] = false;
            }

            return currentPlayers;
        }

        public DSRPlayer UpdateCurrentPlayer(DSRPlayer player)
        {
            return UpdatePlayer(player, CurrentPlayerPtrs);
        }

        public DSRPlayer UpdateRecentPlayer(DSRPlayer player)
        {
            return UpdatePlayer(player, RecentPlayerPtrs);
        }

        public DSRPlayer UpdatePlayer(DSRPlayer player, PHPointer[] playerPtr)
        {
            int index = player.PlayerIndex;

            player.Name = RecentPlayerPtrs[index].ReadString((int)DSROffsets.PlayerGameData.NameString1,
                System.Text.Encoding.Unicode, 32, false);
            player.SoulLevel = RecentPlayerPtrs[index].ReadInt32((int)DSROffsets.PlayerGameData.SoulLevel);
            player.Vitality = RecentPlayerPtrs[index].ReadInt32((int)DSROffsets.PlayerGameData.Vitality);
            player.Attunement = RecentPlayerPtrs[index].ReadInt32((int)DSROffsets.PlayerGameData.Attunement);
            player.Endurance = RecentPlayerPtrs[index].ReadInt32((int)DSROffsets.PlayerGameData.Endurance);
            player.Strength = RecentPlayerPtrs[index].ReadInt32((int)DSROffsets.PlayerGameData.Strength);
            player.Dexterity = RecentPlayerPtrs[index].ReadInt32((int)DSROffsets.PlayerGameData.Dexterity);
            player.Resistance = RecentPlayerPtrs[index].ReadInt32((int)DSROffsets.PlayerGameData.Resistance);
            player.Intelligence = RecentPlayerPtrs[index].ReadInt32((int)DSROffsets.PlayerGameData.Intelligence);
            player.Faith = RecentPlayerPtrs[index].ReadInt32((int)DSROffsets.PlayerGameData.Faith);
            player.Humanity = RecentPlayerPtrs[index].ReadInt32((int)DSROffsets.PlayerGameData.Humanity);

            return player;
        }

        public void LeaveSession()
        {
            byte[] asm = (byte[])DSRAssembly.LeaveSession.Clone();
            Execute(asm);
        }

        public void KickPlayer(DSRPlayer player)
        {
            byte index = (byte)(player.PlayerIndex + 1);
            byte[] asm = (byte[])DSRAssembly.KickPlayer.Clone();
            byte[] bytes = BitConverter.GetBytes(0x10044000 + index);
            Array.Copy(bytes, 0, asm, 0x1, 4);
            Execute(asm);
        }


        #endregion
    }
}
