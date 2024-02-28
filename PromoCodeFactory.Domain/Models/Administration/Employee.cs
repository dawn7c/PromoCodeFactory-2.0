namespace PromoCodeFactory.Domain.Models.Administration
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public Role Role { get; set; }
        public Guid RoleId { get; set; }

        public Employee(string firstName, string lastName, string email, Guid roleId)
        {
            Id = Guid.NewGuid();    
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            RoleId = roleId;
        }
    }
}
