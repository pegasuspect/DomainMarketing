﻿using System.ComponentModel;

namespace DealerSafe.DTO.Membership
{
    public class ReqChangeEmailValidate
    {
        [Description("Member Number of the member")]
        public int MemberId { get; set; }

        [Description("Security code of the member")]
        public string SecurityCode { get; set; }

        [Description("Old email of the member")]
        public string OldEmail { get; set; }

        [Description("New email of the member")]
        public string NewEmail { get; set; }

        [Description("Check live mail kontrol")]
        public bool CheckLiveEmail { get; set; }

    }
}
