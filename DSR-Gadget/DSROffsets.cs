using System;
using System.Collections.Generic;

namespace DSR_Gadget
{
    internal class DSROffsets
    {
        public const string CamManBaseAOB = "48 8B 05 ? ? ? ? 48 63 D1 48 8B 44 D0 08 C3";
        public const int CamManOffset = 0x10;

        public const string ChrFollowCamAOB = "48 8B 0D ? ? ? ? E8 ? ? ? ? 48 8B 4E 68 48 8B 05 ? ? ? ? 48 89 48 60";
        public const int ChrFollowCamOffset1 = 0;
        public const int ChrFollowCamOffset2 = 0x60;
        public const int ChrFollowCamOffset3 = 0x60;

        public const string GroupMaskAOB = "80 3D ? ? ? ? 00 BE 00 00 00 80";
        public enum GroupMask
        {
            Map = 0x0,
            Objects = 0x1,
            Characters = 0x2,
            SFX = 0x3,
            Cutscenes = 0x4,
        }

        public const string GraphicsDataAOB = "48 8B 05 ? ? ? ? 48 8B 48 08 48 8B 01 48 8B 40 58";
        public const int GraphicsDataOffset1 = 0;
        public const int GraphicsDataOffset2 = 0x738;
        public enum GraphicsData
        {
            FilterOverride = 0x34D,
            FilterBrightnessR = 0x350,
            FilterBrightnessG = 0x354,
            FilterBrightnessB = 0x358,
            FilterSaturation = 0x35C,
            FilterContrastR = 0x360,
            FilterContrastG = 0x364,
            FilterContrastB = 0x368,
            FilterHue = 0x36C,
        }

        public const string ChrClassWarpAOB = "48 8B 05 ? ? ? ? 66 0F 7F 80 ? ? ? ? 0F 28 02 66 0F 7F 80 ? ? ? ? C6 80";
        public const int ChrClassWarpOffset1 = 0;
        public int ChrClassWarpBoost = 0x0;
        public enum ChrClassWarp
        {
            LastBonfire = 0xB24,
            StableX = 0xB90,
            StableY = 0xB94,
            StableZ = 0xB98,
            StableAngle = 0xBA4,
            InitialX = 0xA70,
            InitialY = 0xA74,
            InitialZ = 0xA78,
            InitialAngle = 0x84,
        }

        public const string WorldChrBaseAOB = "48 8B 05 ? ? ? ? 48 8B 48 68 48 85 C9 0F 84 ? ? ? ? 48 39 5E 10 0F 84 ? ? ? ? 48";
        public const int WorldChrBaseOffset1 = 0;
        public enum WorldChrBase
        {
            ChrData1 = 0x68,
            DeathCam = 0x70,
        }

        public int ChrData1Boost1 = 0x0;
        public int ChrData1Boost2 = 0x0;
        public enum ChrData1
        {
            CurrentPlayers = 0x18, //change and use Boost variables
            ChrMapData = 0x48,
            PlayerCtrl = 0x68, //change and use Boost variables
            ChrType = 0xC4,
            TeamType = 0xC8,
            ChrFlags1 = 0x284,
            MPAreaID = 0x344,
            AreaID = 0x348,
            Health = 0x3D8,
            MaxHealth = 0x3DC,
            Stamina = 0x3E8,
            MaxStamina = 0x3EC,
            ChrFlags2 = 0x514,
        }

        [Flags]
        public enum ChrFlags1
        {
            ForceSetOmission = 0x00000001,
            DisableHPGauge = 0x00000008,
            SetEventGenerate = 0x00000010,
            ForcePlayAnimation = 0x00000100,
            ForceUpdateNextFrame = 0x00000200,
            NoGravity = 0x00004000,
            SetSuperArmor = 0x00010000,
            FirstPerson = 0x00100000,
            ToggleDraw = 0x00800000,
            // Can't die, but still take damage from killboxes and trigger deathcams
            SetDeadMode = 0x02000000,
            // Doesn't prevent healing
            DisableDamage = 0x04000000,
            // Super armor and disable damage, still die to killbox
            EnableInvincible = 0x08000000,
        }

        [Flags]
        public enum ChrFlags2
        {
            DrawHit = 0x00000004,
            // Can't die, killboxes and death cams do nothing
            NoDead = 0x00000020,
            // Also prevents healing
            NoDamage = 0x00000040,
            NoHit = 0x00000080,
            NoAttack = 0x00000100,
            NoMove = 0x00000200,
            NoStaminaConsumption = 0x00000400,
            NoMpConsumption = 0x00000800,
            DrawDirection = 0x00004000,
            NoUpdate = 0x00008000,
            DrawCounter = 0x00200000,
            NoGoodsConsume = 0x01000000,
        }

        public enum ChrMapData
        {
            ChrAnimData = 0x18,
            ChrPosData = 0x28,
            ChrMapFlags = 0x104,
            Warp = 0x108,
            WarpX = 0x110,
            WarpY = 0x114,
            WarpZ = 0x118,
            WarpAngle = 0x124,
        }

        [Flags]
        public enum ChrMapFlags : uint
        {
            DisableMapHit = 0x00000010,
        }

        public enum ChrAnimData
        {
            AnimSpeed = 0xA8,
        }

        public enum ChrPosData
        {
            PosAngle = 0x4,
            PosX = 0x10,
            PosY = 0x14,
            PosZ = 0x18,
        }

        public enum PlayerCtrl
        {
            ActionCtrl = 0x48,
        }

        public enum ActionCtrl
        {
            CurrentAnimation = 0x80,
        }

        public const int CurrentPlayerOffset = 0x38;
        public enum CurrentPlayers
        {
            CurrentPlayer1 = 0x38,
            CurrentPlayer2 = 0x70,
            CurrentPlayer3 = 0xA8,
            CurrentPlayer4 = 0xE0,
            CurrentPlayer5 = 0x118,
        }
        public const int CurrentPlayersOffset = 0x578;

        public const string ChrDbgAOB = "80 3D ? ? ? ? 00 48 8B 8F ? ? ? ? 0F B6 DB";
        public enum ChrDbg
        {
            PlayerNoDead = 0x0,
            PlayerExterminate = 0x1,
            AllNoStaminaConsume = 0x2,
            AllNoMpConsume = 0x3,
            AllNoArrowConsume = 0x4,
            AllNoMagicQtyConsume = 0x5,
            PlayerHide = 0x6,
            PlayerSilence = 0x7,
            AllNoDead = 0x8,
            AllNoDamage = 0x9,
            AllNoHit = 0xA,
            AllNoAttack = 0xB,
            AllNoMove = 0xC,
            AllNoUpdateAI = 0xD,
            PlayerReload = 0x12,
        }

        public const string MenuManAOB = "48 8B 05 ? ? ? ? 89 88 28 08 00 00 85 C9 ? ? C7 80 34 08 00 00 FF FF FF FF C3";
        public const int MenuManOffset1 = 0;
        public enum MenuMan
        {
            MenuKick = 0x24C,
        }

        public const string GameDataManAOB = "48 8B 05 ? ? ? ? 48 85 C0 ? ? F3 0F 58 80 AC 00 00 00";
        public const int GameDataManOffset1 = 0;
        public enum GameDataMan
        {
            PlayerGameData = 0x10,
            PlayerGameDataRecent = 0x18,
            LastBloodstainPos = 0x40,
            PlayTime = 0xA4,
        }

        public enum PlayerGameData
        {
            Hp = 0x14,
            MaxHp = 0x18,
            BaseMaxHp = 0x1C,

            Stamina = 0x30,
            MaxStamina = 0x34,
            BaseMaxStamina = 0x38,

            Vitality = 0x40,
            Attunement = 0x48,
            Endurance = 0x50,
            Strength = 0x58,
            Dexterity = 0x60,
            Intelligence = 0x68,
            Faith = 0x70,
            Humanity = 0x84,
            Resistance = 0x88,
            SoulLevel = 0x90,
            Souls = 0x94,

            NameString1 = 0xA8,

            Gender = 0xCA,
            Class = 0xCE,
            Physique = 0xCF,

            MultiplayerCount = 0xD4,
            CoopSuccessCount = 0xD8,


            WarriorOfSunlight = 0xED,
            DarkWraith = 0xEE,
            PathOfTheDragon = 0xEF,
            GravelordServant = 0xF0,
            ForestHunter = 0xF1,
            DarkmoonBlade = 0xF2,
            ChaosServant = 0xF3,

            CurrentCovenant = 0x113,
            InvadeType = 0x118,
            WeaponMemory = 0x119,
            EstusLevel = 0x11A,

            NameString2 = 0x12C, //(used to display name to other players)

            LeftWep1 = 0x324,
            RightWep1 = 0x328,
            LeftWep2 = 0x32C,
            RightWep2 = 0x330,
            Arrow1 = 0x334,
            Bolt1 = 0x338,
            Arrow2 = 0x33C,
            Bolt2 = 0x340,
            ArmorHead = 0x344,
            ArmorChest = 0x348,
            ArmorHands = 0x34C,
            ArmorLegs = 0x350,
            Hair = 0x354,
            Ring1 = 0x358,
            Ring2 = 0x35C,
            QuickBar1 = 0x360,
            QuickBar2 = 0x364,
            QuickBar3 = 0x368,
            QuickBar4 = 0x36C,
            QuickBar5 = 0x370,

            HeadSize = 0x388,
            ChestSize = 0x38C,
            AbdomenSize = 0x390,
            ArmSize = 0x394,
            LegSize = 0x398,

            EquipMagicData = 0x418,
            GestureEquipData = 0x450,

            HairRed = 0x4C0,
            HairGreen = 0x4C4,
            HairBlue = 0x4C8,
            HairAlpha = 0x4CC,

            EyeRed = 0x4C0,
            EyeGreen = 0x4C4,
            EyeBlue = 0x4C8,

            FaceDataStart = 0x4E0,
            SkinColorStart = 0x512,

            GestureGameData = 0x568,
        }

        public enum EquipMagicData
        {
            AttunementSlot1 = 0x18,
            Quantity1 = 0x1C,
        }

        public enum GestureGameData
        {
            PointForward = 0x10,
            PointUp = 0x14,
            PointDown = 0x18,
            Beckon = 0x1C,
            Wave = 0x20,
            Bow = 0x24,
            ProperBow = 0x28,
            Hurrah = 0x2C,
            Joy = 0x30,
            Shrug = 0x34,
            LookSkyward = 0x38,
            WellWhatIsIt = 0x3C,
            Prostration = 0x40,
            Prayer = 0x44,
            PraiseTheSun = 0x48,
        }

        public enum LastBloodstainPos
        {
            PosX = 0x0,
            PosY = 0x4,
            PosZ = 0x8,
            PosAngle = 0x10,
        }

        public const int RecentPlayerOffset = 0x660;
        public enum PlayerGameDataRecent
        {
            // RecentPlayer is instance of PlayerGameData
            RecentPlayer1 = 0x460,
            RecentPlayer2 = 0x460 + RecentPlayerOffset,
            RecentPlayer3 = 0x460 + RecentPlayerOffset * 2,
            RecentPlayer4 = 0x460 + RecentPlayerOffset * 3,
            RecentPlayer5 = 0x460 + RecentPlayerOffset * 4,

        }

        public const string EventFlagsAOB = "48 8B 0D ? ? ? ? 99 33 C2 45 33 C0 2B C2 8D 50 F6";
        public const int EventFlagsOffset1 = 0;
        public const int EventFlagsOffset2 = 0;

        public const string ItemGetAOB = "48 89 5C 24 18 89 54 24 10 55 56 57 41 54 41 55 41 56 41 57 48 8D 6C 24 F9";

        public const string BonfireWarpAOB = "48 89 5C 24 08 57 48 83 EC 20 48 8B D9 8B FA 48 8B 49 08 48 85 C9 0F 84 ? ? ? ? E8 ? ? ? ? 48 8B 4B 08";

        public const string DurabilityAOB = "41 8D 41 FF 45 8B C3 45 33 C9";
        public const string DurabilitySpecialAOB = "49 03 49 38 44 89 41 14 C3 32 C0";

        public static DSROffsets GetOffsets(int moduleSize)
        {
            DSROffsets result = new DSROffsets();
            int version = versions.ContainsKey(moduleSize) ? versions[moduleSize] : 100;

            if (version > 1)
            {
                result.ChrClassWarpBoost = 0x10;
            }

            if (version > 2)
            {
                result.ChrData1Boost1 = 0x20;
                result.ChrData1Boost2 = 0x10;
            }

            return result;
        }

        private static readonly Dictionary<int, int> versions = new Dictionary<int, int>()
        {
            [0x4869400] = 0, // 1.01
            [0x496BE00] = 1, // 1.01.1
            [0x37CB400] = 2, // 1.01.2
            [0x3817800] = 3, // 1.03
        };
    }
}
