using PropertyHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSR_Gadget.List_Items.DSRParam
{
    public class DSREquipParamWeapon : DSRParam
    {
        public DSREquipParamWeapon(PHPointer paramPtr, PHook hook) : base(paramPtr, hook)
        {
            FlpContent.Add(new FlpEntry<NumericUpDown>(new NumericUpDown(), "ResidentSpEffectID0", p =>
            {

            }, p =>
            {

            }).Flp);
        }

        public int ResidentSpEffectID0
        {
            get => ItemParamPtr.ReadInt32((int)DSROffsets.EquipParamWeapon.ResidentSpEffectID0);
            set => ItemParamPtr.WriteInt32((int)DSROffsets.EquipParamWeapon.ResidentSpEffectID0, value);
        }
    }
}
