using PromoCodeFactory.Domain.Models.Administration;
using System.ComponentModel.DataAnnotations;

namespace PromoCodeFactory.Infrastructure.Models
{
    public class PartnerRequest
    {
        public string Company {  get; set; }
        public Guid id { get; set; }
    }
}
