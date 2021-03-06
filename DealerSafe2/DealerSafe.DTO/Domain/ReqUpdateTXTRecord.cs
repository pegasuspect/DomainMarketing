﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DealerSafe.DTO.Domain
{
    public class ReqUpdateTXTRecord
    {
        [Description("Domain Name")]
        public string DomainName { get; set; }
        
        [Description("Old Record Name")]
        public string OldRecordName { get; set; }

        [Description("Old Txt Record")]
        public string[] OldTxtRecord { get; set; }

        [Description("New Txt Record")]
        public string[] NewTxtRecord { get; set; }

        [Description("Simple Dns'in calisacagi server id'si")]
        public int? ServerId { get; set; }

        [Description("Power Dns mi")]
        public bool? isPower { get; set; }
    }
}
