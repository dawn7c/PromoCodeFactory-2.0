using PromoCodeFactory.Domain.Models.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodeFactory.Domain.Models.PromoCode_Management
{
    public class Partner : BaseEntity
    {
        [MaxLength(15)]
        public string PartnerName { get; set; }
        public Employee PartnerManager { get; set; }
        public Guid PartnerManagerId { get; set; }
        public Guid PromoCodeId { get; set; }
        public List<PromoCode> PromoCodes { get; set; }
    }
}
