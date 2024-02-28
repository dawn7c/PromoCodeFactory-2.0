﻿namespace PromoCodeFactory.Domain.Models.PromoCode_Management
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public virtual ICollection<PromoCode> PromoCodes { get; set; } = new List<PromoCode>();
        public List<Preference> Preferences { get; set; }

    }
}
