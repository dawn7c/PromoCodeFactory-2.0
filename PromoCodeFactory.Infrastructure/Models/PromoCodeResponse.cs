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
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PartnerName { get; set; }
        public string namePreference {  get; set; }
        public Guid PartnerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string FullName => $"{FirstName} {LastName}";

        //public string partnerName { get; set; }

    }
}
