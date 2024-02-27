using PromoCodeFactory.Domain.Models.Administration;

namespace PromoCodeFactory.Infrastructure.Models
{
    public class EmployeeResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
       // public Role Role { get; set; }
        public Guid RoleId { get; set; }
        //public int AppliedPromocodesCount { get; set; }
    }
}
