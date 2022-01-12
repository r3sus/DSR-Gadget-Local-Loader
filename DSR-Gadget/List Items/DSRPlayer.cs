using System;
using System.Globalization;
using PropertyHook;

namespace DSR_Gadget
{
    public class DSRPlayer
    {
        public PHPointer PlayerPtr { get; set; }
        public PHPointer PlayerInsPtr { get; set; }
        private PHPointer PlayerCtrlPtr;
        private PHPointer ChrPosDataPtr;
        private PHPointer ActionCtrlPtr;
        private PHPointer ChrAnimDataPtr;
        private PHPointer PlayerGameDataPtr;
        private PHPointer GestureGameDataPtr;
        private PHPointer GestureEquipDataPtr;
        private PHPointer SteamPlayerDataPtr;
        private PHPointer SteamOnlineIDDataPtr;
        private PHPointer PosLock;

        private PlayerDataType DataType;

        public string NameString1
        {
            get => PlayerGameDataPtr.ReadString((int)DSROffsets.PlayerGameData.NameString1,
                System.Text.Encoding.Unicode, 32, true);
            set => PlayerGameDataPtr.WriteString((int)DSROffsets.PlayerGameData.NameString1,
                System.Text.Encoding.Unicode, 30, value);
        }

        public string NameString2
        {
            get => PlayerGameDataPtr.ReadString((int)DSROffsets.PlayerGameData.NameString2,
                System.Text.Encoding.Unicode, 32, true);
            set => PlayerGameDataPtr.WriteString((int)DSROffsets.PlayerGameData.NameString2,
                System.Text.Encoding.Unicode, 30, value);
        }

        public string SteamName
        {
            get => SteamPlayerDataPtr.ReadString((int)DSROffsets.SteamPlayerData.Name, System.Text.Encoding.Unicode, 64);
        }

        public string SteamID64Hex
        {
            get => SteamOnlineIDDataPtr.ReadString((int)DSROffsets.SteamOnlineIDData.SteamID64, System.Text.Encoding.Unicode, 32);
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

        #region Stats

        public int Hp
        {
            get => PlayerInsPtr.ReadInt32((int)DSROffsets.PlayerIns.Health);
            set => PlayerInsPtr.WriteInt32((int)DSROffsets.PlayerIns.Health, value);
        }

        public int MaxHp
        {
            get => PlayerInsPtr.ReadInt32((int)DSROffsets.PlayerIns.MaxHealth);
        }

        public int Stamina
        {
            get => PlayerInsPtr.ReadInt32((int)DSROffsets.PlayerIns.Stamina);
            set => PlayerInsPtr.WriteInt32((int)DSROffsets.PlayerIns.Stamina, value);
        }

        public int MaxStamina
        {
            get => PlayerInsPtr.ReadInt32((int)DSROffsets.PlayerIns.MaxStamina);
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

        public int Humanity
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.Humanity);
            set => PlayerGameDataPtr.WriteInt32((int) DSROffsets.PlayerGameData.Humanity, value);
        }

        #endregion stats

        public int ChrType
        {
            get => PlayerInsPtr.ReadInt32((int)DSROffsets.PlayerIns.ChrType);
            set => PlayerInsPtr.WriteInt32((int)DSROffsets.PlayerIns.ChrType, value);
        }

        public int TeamType
        {
            get => PlayerInsPtr.ReadInt32((int)DSROffsets.PlayerIns.TeamType);
            set => PlayerInsPtr.WriteInt32((int)DSROffsets.PlayerIns.TeamType, value);
        }

        public byte InvadeType
        {
            get => PlayerGameDataPtr.ReadByte((int)DSROffsets.PlayerGameData.InvadeType);
            set => PlayerGameDataPtr.WriteByte((int)DSROffsets.PlayerGameData.InvadeType, value);
        }

        #region Location

        public int MPAreaID
        {
            get => PlayerInsPtr.ReadInt32((int)DSROffsets.PlayerIns.MPAreaID);
            set => PlayerInsPtr.WriteInt32((int)DSROffsets.PlayerIns.MPAreaID, value);
        }

        public int AreaID
        {
            get => PlayerInsPtr.ReadInt32((int)DSROffsets.PlayerIns.AreaID);
            set => PlayerInsPtr.WriteInt32((int)DSROffsets.PlayerIns.AreaID, value);
        }

        public float PosX
        {
            get => ChrPosDataPtr.ReadSingle((int)DSROffsets.ChrPosData.PosX);
            set => ChrPosDataPtr.WriteSingle((int)DSROffsets.ChrPosData.PosX, value);
        }
        public float PosY
        {
            get => ChrPosDataPtr.ReadSingle((int)DSROffsets.ChrPosData.PosY);
            set => ChrPosDataPtr.WriteSingle((int)DSROffsets.ChrPosData.PosY, value);
        }
        public float PosZ
        {
            get => ChrPosDataPtr.ReadSingle((int)DSROffsets.ChrPosData.PosZ);
            set => ChrPosDataPtr.WriteSingle((int)DSROffsets.ChrPosData.PosZ, value);
        }
        public float PosAngle
        {
            get => ChrPosDataPtr.ReadSingle((int)DSROffsets.ChrPosData.PosAngle);
            set => ChrPosDataPtr.WriteSingle((int)DSROffsets.ChrPosData.PosAngle, value);
        }

        public void PosWarp(Position pos)
        {
            PlayerCtrlPtr.WriteSingle((int)DSROffsets.PlayerCtrl.WarpX, pos.X);
            PlayerCtrlPtr.WriteSingle((int)DSROffsets.PlayerCtrl.WarpY, pos.Y);
            PlayerCtrlPtr.WriteSingle((int)DSROffsets.PlayerCtrl.WarpZ, pos.Z);
            PlayerCtrlPtr.WriteSingle((int)DSROffsets.PlayerCtrl.WarpAngle, pos.Angle);
            PlayerCtrlPtr.WriteBoolean((int)DSROffsets.PlayerCtrl.Warp, true);
        }

        public void PosWarpLock(float x, float y, float z)
        {
            PosX = x;
            PosY = y;
            PosZ = z;
        }

        public void SetPosLock(bool enable)
        {
            if (enable)
            {
                PosLock.WriteBytes((int)DSROffsets.PosLock.PosLock, new byte[] { 0x90, 0x90, 0x90, 0x90 });
            }
            else
            {
                PosLock.WriteBytes((int)DSROffsets.PosLock.PosLock, new byte[] { 0x0F, 0x29, 0x5B, 0x10 });
            }
        }
        public Position GetPosition()
        {
            return new Position(PosX, PosY, PosZ, PosAngle);
        }

        public struct Position
        {

            public float X;
            public float Y;
            public float Z;
            public float Angle;

            public Position(float x, float y, float z, float angle)
            {
                X = x;
                Y = y;
                Z = z;
                Angle = angle;
            }
        }

        #endregion

        public int CurrentAnimation
        {
            get => ActionCtrlPtr.ReadInt32((int)DSROffsets.ActionCtrl.CurrentAnimation);
            set => ActionCtrlPtr.WriteInt32((int)DSROffsets.ActionCtrl.CurrentAnimation, value);
        }

        public float AnimSpeed
        {
            set => ChrAnimDataPtr.WriteSingle((int)DSROffsets.ChrAnimData.AnimSpeed, value);
        }

        public byte Class
        {
            get => PlayerGameDataPtr.ReadByte((int)DSROffsets.PlayerGameData.Class);
            set => PlayerGameDataPtr.WriteByte((int)DSROffsets.PlayerGameData.Class, value);
        }

        public int Souls
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.Souls);
            set => PlayerGameDataPtr.WriteInt32((int)DSROffsets.PlayerGameData.Souls, value);
        }

        public byte WeaponMemory
        {
            get => PlayerGameDataPtr.ReadByte((int)DSROffsets.PlayerGameData.WeaponMemory);
            set => PlayerGameDataPtr.WriteByte((int)DSROffsets.PlayerGameData.WeaponMemory, value);
        }

        public int Indictments
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.Indictments);
            set => PlayerGameDataPtr.WriteInt32((int)DSROffsets.PlayerGameData.Indictments, value);
        }

        #region Covenant

        public byte Covenant
        {
            get => PlayerGameDataPtr.ReadByte((int)DSROffsets.PlayerGameData.CurrentCovenant);
            set => PlayerGameDataPtr.WriteByte((int)DSROffsets.PlayerGameData.CurrentCovenant, value);
        }

        public byte WarriorOfSunlight
        {
            get => PlayerGameDataPtr.ReadByte((int)DSROffsets.PlayerGameData.WarriorOfSunlight);
            set => PlayerGameDataPtr.WriteByte((int)DSROffsets.PlayerGameData.WarriorOfSunlight, value);
        }

        public byte Darkwraith
        {
            get => PlayerGameDataPtr.ReadByte((int)DSROffsets.PlayerGameData.Darkwraith);
            set => PlayerGameDataPtr.WriteByte((int)DSROffsets.PlayerGameData.Darkwraith, value);
        }

        public byte PathOfTheDragon
        {
            get => PlayerGameDataPtr.ReadByte((int)DSROffsets.PlayerGameData.PathOfTheDragon);
            set => PlayerGameDataPtr.WriteByte((int)DSROffsets.PlayerGameData.PathOfTheDragon, value);
        }

        public byte GravelordServant
        {
            get => PlayerGameDataPtr.ReadByte((int)DSROffsets.PlayerGameData.GravelordServant);
            set => PlayerGameDataPtr.WriteByte((int)DSROffsets.PlayerGameData.GravelordServant, value);
        }

        public byte ForestHunter
        {
            get => PlayerGameDataPtr.ReadByte((int)DSROffsets.PlayerGameData.ForestHunter);
            set => PlayerGameDataPtr.WriteByte((int)DSROffsets.PlayerGameData.ForestHunter, value);
        }

        public byte DarkmoonBlade
        {
            get => PlayerGameDataPtr.ReadByte((int)DSROffsets.PlayerGameData.DarkmoonBlade);
            set => PlayerGameDataPtr.WriteByte((int)DSROffsets.PlayerGameData.DarkmoonBlade, value);
        }

        public byte ChaosServant
        {
            get => PlayerGameDataPtr.ReadByte((int)DSROffsets.PlayerGameData.ChaosServant);
            set => PlayerGameDataPtr.WriteByte((int)DSROffsets.PlayerGameData.ChaosServant, value);
        }

        #endregion

        #region Fashion

        public int Hair
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.Hair);
            set => PlayerGameDataPtr.WriteInt32((int)DSROffsets.PlayerGameData.Hair, value);
        }

        public float HairRed
        {
            get => PlayerGameDataPtr.ReadSingle((int)DSROffsets.PlayerGameData.HairRed);
            set => PlayerGameDataPtr.WriteSingle((int)DSROffsets.PlayerGameData.HairRed, value);
        }

        public float HairGreen
        {
            get => PlayerGameDataPtr.ReadSingle((int)DSROffsets.PlayerGameData.HairGreen);
            set => PlayerGameDataPtr.WriteSingle((int)DSROffsets.PlayerGameData.HairGreen, value);
        }

        public float HairBlue
        {
            get => PlayerGameDataPtr.ReadSingle((int)DSROffsets.PlayerGameData.HairBlue);
            set => PlayerGameDataPtr.WriteSingle((int)DSROffsets.PlayerGameData.HairBlue, value);
        }

        public float HairAlpha
        {
            get => PlayerGameDataPtr.ReadSingle((int)DSROffsets.PlayerGameData.HairAlpha);
            set => PlayerGameDataPtr.WriteSingle((int)DSROffsets.PlayerGameData.HairAlpha, value);
        }

        public float EyeRed
        {
            get => PlayerGameDataPtr.ReadSingle((int)DSROffsets.PlayerGameData.EyeRed);
            set => PlayerGameDataPtr.WriteSingle((int)DSROffsets.PlayerGameData.EyeRed, value);
        }

        public float EyeGreen
        {
            get => PlayerGameDataPtr.ReadSingle((int)DSROffsets.PlayerGameData.EyeGreen);
            set => PlayerGameDataPtr.WriteSingle((int)DSROffsets.PlayerGameData.EyeGreen, value);
        }

        public float EyeBlue
        {
            get => PlayerGameDataPtr.ReadSingle((int)DSROffsets.PlayerGameData.EyeBlue);
            set => PlayerGameDataPtr.WriteSingle((int)DSROffsets.PlayerGameData.EyeBlue, value);
        }

        public byte Gender
        {
            get => PlayerGameDataPtr.ReadByte((int)DSROffsets.PlayerGameData.Gender);
            set => PlayerGameDataPtr.WriteByte((int)DSROffsets.PlayerGameData.Gender, value);
        }

        public byte Physique
        {
            get => PlayerGameDataPtr.ReadByte((int)DSROffsets.PlayerGameData.Physique);
            set => PlayerGameDataPtr.WriteByte((int)DSROffsets.PlayerGameData.Physique, value);
        }

        public byte HeadSize
        {
            get => PlayerGameDataPtr.ReadByte((int)DSROffsets.PlayerGameData.HeadSize);
            set => PlayerGameDataPtr.WriteByte((int)DSROffsets.PlayerGameData.HeadSize, value);
        }

        public byte ChestSize
        {
            get => PlayerGameDataPtr.ReadByte((int)DSROffsets.PlayerGameData.ChestSize);
            set => PlayerGameDataPtr.WriteByte((int)DSROffsets.PlayerGameData.ChestSize, value);
        }

        public byte AbdomenSize
        {
            get => PlayerGameDataPtr.ReadByte((int)DSROffsets.PlayerGameData.AbdomenSize);
            set => PlayerGameDataPtr.WriteByte((int)DSROffsets.PlayerGameData.AbdomenSize, value);
        }

        public byte ArmSize
        {
            get => PlayerGameDataPtr.ReadByte((int)DSROffsets.PlayerGameData.ArmSize);
            set => PlayerGameDataPtr.WriteByte((int)DSROffsets.PlayerGameData.ArmSize, value);
        }

        public byte LegSize
        {
            get => PlayerGameDataPtr.ReadByte((int)DSROffsets.PlayerGameData.LegSize);
            set => PlayerGameDataPtr.WriteByte((int)DSROffsets.PlayerGameData.LegSize, value);
        }

        #endregion

        #region Equipment

        public int ArmorHead
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.ArmorHead);
            set => PlayerGameDataPtr.WriteInt32((int)DSROffsets.PlayerGameData.ArmorHead, value);
        }

        public int ArmorChest
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.ArmorChest);
            set => PlayerGameDataPtr.WriteInt32((int)DSROffsets.PlayerGameData.ArmorChest, value);
        }

        public int ArmorHands
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.ArmorHands);
            set => PlayerGameDataPtr.WriteInt32((int)DSROffsets.PlayerGameData.ArmorHands, value);
        }

        public int ArmorLegs
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.ArmorLegs);
            set => PlayerGameDataPtr.WriteInt32((int)DSROffsets.PlayerGameData.ArmorLegs, value);
        }

        public int Arrow1
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.Arrow1);
            set => PlayerGameDataPtr.WriteInt32((int)DSROffsets.PlayerGameData.Arrow1, value);
        }

        public int Arrow2
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.Arrow2);
            set => PlayerGameDataPtr.WriteInt32((int)DSROffsets.PlayerGameData.Arrow2, value);
        }

        public int Bolt1
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.Bolt1);
            set => PlayerGameDataPtr.WriteInt32((int)DSROffsets.PlayerGameData.Bolt1, value);
        }

        public int Bolt2
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.Bolt2);
            set => PlayerGameDataPtr.WriteInt32((int)DSROffsets.PlayerGameData.Bolt2, value);
        }

        public int RightWep1
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.RightWep1);
            set => PlayerGameDataPtr.WriteInt32((int)DSROffsets.PlayerGameData.RightWep1, value);
        }

        public int RightWep2
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.RightWep2);
            set => PlayerGameDataPtr.WriteInt32((int)DSROffsets.PlayerGameData.RightWep2, value);
        }

        public int LeftWep1
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.LeftWep1);
            set => PlayerGameDataPtr.WriteInt32((int)DSROffsets.PlayerGameData.LeftWep1, value);
        }

        public int LeftWep2
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.LeftWep2);
            set => PlayerGameDataPtr.WriteInt32((int)DSROffsets.PlayerGameData.LeftWep2, value);
        }

        public int Ring1
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.Ring1);
            set => PlayerGameDataPtr.WriteInt32((int)DSROffsets.PlayerGameData.Ring1, value);
        }

        public int Ring2
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.Ring2);
            set => PlayerGameDataPtr.WriteInt32((int)DSROffsets.PlayerGameData.Ring2, value);
        }

        public int Quickbar1
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.Quickbar1);
            set => PlayerGameDataPtr.WriteInt32((int)DSROffsets.PlayerGameData.Quickbar1, value);
        }

        public int Quickbar2
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.Quickbar2);
            set => PlayerGameDataPtr.WriteInt32((int)DSROffsets.PlayerGameData.Quickbar2, value);
        }

        public int Quickbar3
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.Quickbar3);
            set => PlayerGameDataPtr.WriteInt32((int)DSROffsets.PlayerGameData.Quickbar3, value);
        }

        public int Quickbar4
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.Quickbar4);
            set => PlayerGameDataPtr.WriteInt32((int)DSROffsets.PlayerGameData.Quickbar4, value);
        }

        public int Quickbar5
        {
            get => PlayerGameDataPtr.ReadInt32((int)DSROffsets.PlayerGameData.Quickbar5);
            set => PlayerGameDataPtr.WriteInt32((int)DSROffsets.PlayerGameData.Quickbar5, value);
        }

        #endregion

        #region Gestures
        public bool GesturePointForward
        {
            get => GestureGameDataPtr.ReadByte((int)DSROffsets.GestureGameData.PointForward) == 3;
            set => GestureGameDataPtr.WriteByte((int)DSROffsets.GestureGameData.PointForward, value ? (byte)3 : (byte)2);
        }

        public bool GesturePointUp
        {
            get => GestureGameDataPtr.ReadByte((int)DSROffsets.GestureGameData.PointUp) == 5;
            set => GestureGameDataPtr.WriteByte((int)DSROffsets.GestureGameData.PointUp, value ? (byte)5 : (byte)4);
        }

        public bool GesturePointDown
        {
            get => GestureGameDataPtr.ReadByte((int)DSROffsets.GestureGameData.PointDown) == 7;
            set => GestureGameDataPtr.WriteByte((int)DSROffsets.GestureGameData.PointDown, value ? (byte)7 : (byte)6);
        }

        public bool GestureBeckon
        {
            get => GestureGameDataPtr.ReadByte((int)DSROffsets.GestureGameData.Beckon) == 9;
            set => GestureGameDataPtr.WriteByte((int)DSROffsets.GestureGameData.Beckon, value ? (byte)9 : (byte)8);
        }

        public bool GestureWave
        {
            get => GestureGameDataPtr.ReadByte((int)DSROffsets.GestureGameData.Wave) == 11;
            set => GestureGameDataPtr.WriteByte((int)DSROffsets.GestureGameData.Wave, value ? (byte)11 : (byte)10);
        }

        public bool GestureBow
        {
            get => GestureGameDataPtr.ReadByte((int)DSROffsets.GestureGameData.Bow) == 13;
            set => GestureGameDataPtr.WriteByte((int)DSROffsets.GestureGameData.Bow, value ? (byte)13 : (byte)12);
        }

        public bool GestureProperBow
        {
            get => GestureGameDataPtr.ReadByte((int)DSROffsets.GestureGameData.ProperBow) == 15;
            set => GestureGameDataPtr.WriteByte((int)DSROffsets.GestureGameData.ProperBow, value ? (byte)15 : (byte)14);
        }

        public bool GestureHurrah
        {
            get => GestureGameDataPtr.ReadByte((int)DSROffsets.GestureGameData.Hurrah) == 17;
            set => GestureGameDataPtr.WriteByte((int)DSROffsets.GestureGameData.Hurrah, value ? (byte)17 : (byte)16);
        }

        public bool GestureJoy
        {
            get => GestureGameDataPtr.ReadByte((int)DSROffsets.GestureGameData.Joy) == 19;
            set => GestureGameDataPtr.WriteByte((int)DSROffsets.GestureGameData.Joy, value ? (byte)19 : (byte)18);
        }

        public bool GestureShrug
        {
            get => GestureGameDataPtr.ReadByte((int)DSROffsets.GestureGameData.Shrug) == 21;
            set => GestureGameDataPtr.WriteByte((int)DSROffsets.GestureGameData.Shrug, value ? (byte)21 : (byte)20);
        }

        public bool GestureLookSkyward
        {
            get => GestureGameDataPtr.ReadByte((int)DSROffsets.GestureGameData.LookSkyward) == 23;
            set => GestureGameDataPtr.WriteByte((int)DSROffsets.GestureGameData.LookSkyward, value ? (byte)23 : (byte)22);
        }

        public bool GestureWellWhatIsIt
        {
            get => GestureGameDataPtr.ReadByte((int)DSROffsets.GestureGameData.WellWhatIsIt) == 25;
            set => GestureGameDataPtr.WriteByte((int)DSROffsets.GestureGameData.WellWhatIsIt, value ? (byte)25 : (byte)24);
        }

        public bool GestureProstration
        {
            get => GestureGameDataPtr.ReadByte((int)DSROffsets.GestureGameData.Prostration) == 27;
            set => GestureGameDataPtr.WriteByte((int)DSROffsets.GestureGameData.Prostration, value ? (byte)27 : (byte)26);
        }

        public bool GesturePrayer
        {
            get => GestureGameDataPtr.ReadByte((int)DSROffsets.GestureGameData.Prayer) == 29;
            set => GestureGameDataPtr.WriteByte((int)DSROffsets.GestureGameData.Prayer, value ? (byte)29 : (byte)28);
        }

        public bool GesturePraiseTheSun
        {
            get => GestureGameDataPtr.ReadByte((int)DSROffsets.GestureGameData.PraiseTheSun) == 31;
            set => GestureGameDataPtr.WriteByte((int)DSROffsets.GestureGameData.PraiseTheSun, value ? (byte)31 : (byte)30);
        }

        public void GestureUnlockAll()
        {
            bool value = true;
            GesturePointForward = value;
            GesturePointUp = value;
            GesturePointDown = value;
            GestureBeckon = value;
            GestureWave = value;
            GestureBow = value;
            GestureProperBow = value;
            GestureHurrah = value;
            GestureJoy = value;
            GestureShrug = value;
            GestureLookSkyward = value;
            GestureWellWhatIsIt = value;
            GestureProstration = value;
            GesturePrayer = value;
            GesturePraiseTheSun = value;

        }

        public byte GetGestureSlot(int index)
        {
            return GestureEquipDataPtr.ReadByte((int)DSROffsets.GestureEquipData.Slot1 + index * DSROffsets.GestureEquipDataOffset);
        }

        public void SetGestureSlot(int index, byte value)
        {
            GestureEquipDataPtr.WriteByte((int)DSROffsets.GestureEquipData.Slot1 + index * DSROffsets.GestureEquipDataOffset, value);
        }

        #endregion



        public override string ToString()
        {
            if (DataType == PlayerDataType.PlayerIns)
                if (String.IsNullOrEmpty(SteamName))
                    return NameString1;
                else
                    return NameString1 + " (" + SteamName + ")";
            else
                return NameString1;
        }

        public DSRPlayer(PHPointer playerPtr, PlayerDataType dataType, DSRHook dsrHook)
        {
            PlayerPtr = playerPtr;
            DataType = dataType;

            if (DataType == PlayerDataType.PlayerIns)
            {
                PlayerInsPtr = PlayerPtr;
                PlayerGameDataPtr = dsrHook.CreateChildPointer(PlayerPtr, (int)DSROffsets.PlayerIns.PlayerGameData);

                PlayerCtrlPtr = dsrHook.CreateChildPointer(PlayerInsPtr, (int)DSROffsets.PlayerIns.PlayerCtrl);
                ChrPosDataPtr = dsrHook.CreateChildPointer(PlayerCtrlPtr, (int)DSROffsets.PlayerCtrl.ChrPosData);
                ActionCtrlPtr = dsrHook.CreateChildPointer(PlayerCtrlPtr, (int)DSROffsets.PlayerCtrl.ActionCtrl);
                ChrAnimDataPtr = dsrHook.CreateChildPointer(PlayerCtrlPtr, (int)DSROffsets.PlayerCtrl.ChrAnimData);

                PosLock = dsrHook.GetPosLock();



                SteamPlayerDataPtr = dsrHook.CreateChildPointer(PlayerInsPtr, (int)DSROffsets.PlayerIns.SteamPlayerData);
                SteamOnlineIDDataPtr = dsrHook.CreateChildPointer(SteamPlayerDataPtr, (int)DSROffsets.SteamPlayerData.SteamOnlineIDData);

                GestureGameDataPtr = dsrHook.CreateChildPointer(PlayerGameDataPtr, (int)DSROffsets.PlayerGameData.GestureGameData);
                GestureEquipDataPtr = dsrHook.CreateChildPointer(PlayerGameDataPtr, (int)DSROffsets.PlayerGameData.GestureEquipData);

            }
            else
            {
                PlayerGameDataPtr = PlayerPtr;
            }

        }

        public enum PlayerDataType
        {
            PlayerIns,
            PlayerGameData,
        }
    }
}
