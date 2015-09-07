﻿using System;
using System.Collections.Generic;
using Cinar.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DealerSafe2.API.Entity.DomainMarketing
{
    public class DMAutoBidder : BaseEntity
    {

        [ColumnDetail(ColumnType = DbType.VarChar, Length = 12), Description("id of the auction")]
        public string DMAuctionId { get; set; }

        [ColumnDetail(ColumnType = DbType.VarChar, Length = 12), Description("id of the bidder member")]
        public string BidderMemberId { get; set; }

        [Description("the highest bid that member could make")]
        public int MaxBidValue { get; set; }


    }
}