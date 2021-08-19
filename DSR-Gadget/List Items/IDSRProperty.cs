using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSR_Gadget
{
    interface IDSRProperty : IComparable<IDSRProperty>
    {
        int ID { get; set; }
        string Name { get; set; }
    }
}
