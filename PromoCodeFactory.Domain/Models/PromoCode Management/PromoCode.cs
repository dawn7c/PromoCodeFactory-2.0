using System.ComponentModel.DataAnnotations;

namespace PromoCodeFactory.Domain.Models.PromoCode_Management
{
    public class PromoCode : BaseEntity
    {
        [MaxLength(15)]
        public string Code { get; set; }

        
        public string ServiceInfo { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public Preference Preference {  get; set; }
        public Guid PreferenceId {  get; set; }
        public Customer Customer { get; set; }
        public  Guid CustomerId { get; set; }
        public Partner Partner { get; set; }
        public Guid PartnerId { get; set; }

        public PromoCode(string code, string serviceInfo, DateTime beginDate, DateTime endDate, Guid preferenceId, Guid customerId, Guid partnerId)
        {
            Id = Guid.NewGuid();
            Code = code;
            ServiceInfo = serviceInfo;
            BeginDate = beginDate;
            EndDate = endDate;
            PreferenceId = preferenceId;
            CustomerId = customerId;
            PartnerId = partnerId;
        }
    }
}
