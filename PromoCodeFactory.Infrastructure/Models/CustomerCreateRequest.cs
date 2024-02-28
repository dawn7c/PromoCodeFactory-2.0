namespace PromoCodeFactory.Infrastructure.Models
{
    public class CustomerCreateRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid PreferenceId { get; set; }
        
    }
}
