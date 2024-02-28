using PromoCodeFactory.Domain.Models.Administration;
using System.ComponentModel.DataAnnotations;

namespace PromoCodeFactory.Infrastructure.Models
{
    public class PartnerResponse
    {
        
        public string PartnerName {  get; set; }
        public string Company {  get; set; }
        public Guid id {get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }

        //public PartnerResponse(string firstName, string lastName)
        //{
        //    FirstName = firstName;
        //    LastName = lastName;
        //    PartnerName = $"{FirstName}  {LastName}";
        //}
    }
}
