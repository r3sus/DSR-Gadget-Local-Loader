using System;

namespace DSR_Gadget
{
    interface IDSRProperty : IComparable<IDSRProperty>
    {
        int ID { get; set; }
        string Name { get; set; }
    }
}
