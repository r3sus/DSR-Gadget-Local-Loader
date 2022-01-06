namespace DSR_Gadget
{
    public class State
    {
        public struct PlayerState
        {
            public bool Set;
            public int HP, Stamina;
            public bool DeathCam;
            public byte[] FollowCam;
            public int[][] EquipMagicData;
        }
    }
}