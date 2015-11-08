﻿using DealerSafe2.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealerSafe2.API.Entity.DomainMarketing
{
    public class ListViewAuctions : BaseEntity
    {
        public int BiggestBid { get; set; }
        public DMItemTypes Type { get; set; }
        public string DomainName { get; set; }
        public DMAuctionStates Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime PlannedCloseDate { get; set; }
        public int BuyItNowPrice { get; set; }
        public DMSaleStates SaleStatus { get; set; }

    }
}