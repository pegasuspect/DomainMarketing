﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DealerSafe2.DTO.Request
{
    public class ReqGetProfileComplaints: ReqPager
    {
        public string MemberId { get; set; }
    }
}
