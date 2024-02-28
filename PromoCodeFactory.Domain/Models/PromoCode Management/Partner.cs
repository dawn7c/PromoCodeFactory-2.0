using PromoCodeFactory.Domain.Models.Administration;
using System.ComponentModel.DataAnnotations;

namespace PromoCodeFactory.Domain.Models.PromoCode_Management
{
    public class Partner : BaseEntity
    {
        public string Company { get; set; }
        [MaxLength(15)]
        public string PartnerName { get; set; }
        public Employee PartnerManager { get; set; }
        public Guid PartnerManagerId { get; set; }
        public List<PromoCode> PromoCodes { get; set; }

        public Partner(string company, string partnerName, Guid partnerManagerId)
        {
            Company = company;
            PartnerName = partnerName;
            PartnerManagerId = partnerManagerId;
        }
    }
}
