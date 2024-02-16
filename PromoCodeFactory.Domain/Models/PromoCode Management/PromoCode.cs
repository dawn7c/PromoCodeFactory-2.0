using PromoCodeFactory.Domain.Models.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodeFactory.Domain.Models.PromoCode_Management
{
    public class PromoCode : BaseEntity
    {
        [MaxLength(15)]
        public string Code { get; set; }

        [MaxLength(15)]
        public string ServiceInfo { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        [MaxLength(15)]
        public string PartnerName {  get; set; }
        public Employee PartnerManager { get; set; }
        public Guid PartnerManagerId {  get; set; }
        public Preference Preference {  get; set; }
        public Guid PreferenceId {  get; set; }
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }
    }
}
