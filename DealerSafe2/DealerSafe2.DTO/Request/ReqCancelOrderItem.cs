﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerSafe2.DTO.Request
{
   public class ReqCancelOrderItem : BaseRequest
    {
        public string OrderItemId { get; set; }
        public string CancelReason { get; set; }
    }
}
