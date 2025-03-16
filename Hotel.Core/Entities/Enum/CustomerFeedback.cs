using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities.Enum
{
    [Flags]
    public enum CustomerFeedback
    {
        None = 0,
        VeryBad = 1,
        Bad = 2,
        Good = 4,
        VeryGood = 8,
        Excellent = 16
    }
}
