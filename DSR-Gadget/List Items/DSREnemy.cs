using PropertyHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSR_Gadget.List_Items
{
    class DSREnemy
    {
        private PHPointer EnemyInsPtr;

        public bool IsIntPtrZero
        {
            get => EnemyInsPtr.Resolve() == IntPtr.Zero;
        }

        public int Hp
        {
            get => EnemyInsPtr.ReadInt32((int)DSROffsets.EnemyIns.Health);
            set => EnemyInsPtr.WriteInt32((int)DSROffsets.EnemyIns.Health, value);
        }

        public int MaxHp
        {
            get => EnemyInsPtr.ReadInt32((int)DSROffsets.EnemyIns.MaxHealth);
            set => EnemyInsPtr.WriteInt32((int)DSROffsets.EnemyIns.MaxHealth, value);
        }

        public int Stamina
        {
            get => EnemyInsPtr.ReadInt32((int)DSROffsets.EnemyIns.Stamina);
            set => EnemyInsPtr.WriteInt32((int)DSROffsets.EnemyIns.Stamina, value);
        }

        public int MaxStamina
        {
            get => EnemyInsPtr.ReadInt32((int)DSROffsets.EnemyIns.MaxStamina);
            set => EnemyInsPtr.WriteInt32((int)DSROffsets.EnemyIns.MaxStamina, value);
        }

        public int ChrType
        {
            get => EnemyInsPtr.ReadInt32((int)DSROffsets.EnemyIns.ChrType);
            set => EnemyInsPtr.WriteInt32((int)DSROffsets.EnemyIns.ChrType, value);
        }

        public int TeamType
        {
            get => EnemyInsPtr.ReadInt32((int)DSROffsets.EnemyIns.TeamType);
            set => EnemyInsPtr.WriteInt32((int)DSROffsets.EnemyIns.TeamType, value);
        }


        public DSREnemy(PHPointer enemyInsPtr, DSRHook hook)
        {
            EnemyInsPtr = enemyInsPtr;
        }
    }
}
