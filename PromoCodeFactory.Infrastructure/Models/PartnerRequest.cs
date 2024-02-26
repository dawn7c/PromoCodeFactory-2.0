﻿using PromoCodeFactory.Domain.Models.Administration;
using System.ComponentModel.DataAnnotations;

namespace PromoCodeFactory.Infrastructure.Models
{
    public class PartnerRequest
    {
        [MaxLength(15)]
        public string PartnerName { get; set; }
        public Employee PartnerManager { get; set; }
        public Guid PartnerManagerId { get; set; }
    }
}