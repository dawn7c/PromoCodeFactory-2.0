using PromoCodeFactory.Domain.Models.Administration;
using PromoCodeFactory.Domain.Models.PromoCode_Management;
using System.ComponentModel.DataAnnotations;

namespace PromoCodeFactory.Infrastructure.Models
{
    public class PromoCodeResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string ServiceInfo { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public string PartnerName { get; set; }
    }
}
