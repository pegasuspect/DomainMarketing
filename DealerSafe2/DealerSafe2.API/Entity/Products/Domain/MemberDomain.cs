﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cinar.Database;
using DealerSafe2.DTO.EntityInfo.Products.Domain;
using DealerSafe2.DTO.Enums;
using Epp.Protocol.Shared;
using DealerSafe.DTO.Epp.Protocol.Rgp;

namespace DealerSafe2.API.Entity.Products.Domain
{
    public class MemberDomain : BaseEntity 
    {
        [ColumnDetail(Length = 100)]
        public string DomainName { get; set; }
        [ColumnDetail(Length = 100)]
        public string DomainIDN { get; set; }
        [ColumnDetail(Length = 50)]
        public string ROID { get; set; }

        public DateTime UpdateDate { get; set; }

        [ColumnDetail(ColumnType = DbType.VarChar, Length = 20)]
        public DomainRenewalModes RenewalMode { get; set; }
        [ColumnDetail(ColumnType = DbType.VarChar, Length = 20)]
        public DomainTransferModes TransferMode { get; set; }

        [ColumnDetail(Length = 200)]
        public string RegistryStatus { get; set; } // List<RegistryStates>
        public List<Status> RegistryStates
        {
            get
            {
                return RegistryStatus.SplitWithTrim(',').Select(s => (Status)Enum.Parse(typeof(Status), s)).ToList();
            }
            set {
                RegistryStatus = value.StringJoin(",");
            }
        }

        [ColumnDetail(ColumnType = DbType.VarChar, Length = 20)]
        public statusValueType RGPStatus { get; set; }

        public string SponsorId { get; set; }
        public string CreatorId { get; set; }

        [ColumnDetail(Length = 400)]
        // format: IP,Name;IP,Name
        public string NameServers { get; set; }

        [ColumnDetail(Length = 12)]
        public string ZoneId { get; set; } // com, com.tr, computer gibi IDler çok şık olur


        [ColumnDetail(Length = 12)]
        public string OwnerDomainContactId { get; set; } // whois veritabanındaki ID aynen kullanılabilir
        [ColumnDetail(Length = 12)]
        public string AdminDomainContactId { get; set; }
        [ColumnDetail(Length = 12)]
        public string TechDomainContactId { get; set; }
        [ColumnDetail(Length = 12)]
        public string BillingDomainContactId { get; set; }

        [ColumnDetail(ColumnType = DbType.VarChar, Length = 20)]
        public PrivacyProtectionOptions PrivacyProtection { get; set; }
        [ColumnDetail(Length = 20)]
        public string AuthInfo { get; set; } // EppCode transfer şifresi

        [ColumnDetail(ColumnType = DbType.VarChar, Length = 20)]
        public OperationalStates OperationalStatus { get; set; }

        [ColumnDetail(ColumnType = DbType.VarChar, Length = 20)]
        public OperationalStates PriceMode { get; set; }

        public DateTime TransferDate { get; set; }

        public TransferStates TransferState { get; set; }
    }

    public class ListViewMemberDomain : BaseEntity
    {
        public DateTime InsertDate { get; set; }
        public string OrderItemId { get; set; }
        public string OrderId { get; set; }
        public string MemberId { get; set; }
        public string MemberName { get; set; }
        public string ProductName { get; set; }
        public DomainRenewalModes RenewalMode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DomainName { get; set; }
    }
}