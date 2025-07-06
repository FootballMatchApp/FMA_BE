using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMA.Common.Enums
{
    public enum PitchStatus
    {
        AVAILABLE,  // CÓ THỂ ĐẶT
        BOOKED,     // ĐÃ ĐƯỢC ĐẶT
        CLOSED,     // ĐANG ĐÓNG
        UNAVAILABLE // KHÔNG CÓ SẴN
    }
}
