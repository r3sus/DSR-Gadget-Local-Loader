using System;

namespace DSR_Gadget.List_Items
{
    public interface IDSRProperty : IComparable<IDSRProperty>
    {
        int ID { get; set; }
        string Name { get; set; }
    }
}
