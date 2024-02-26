﻿using PromoCodeFactory.Domain.Models.Administration;

namespace PromoCodeFactory.Infrastructure.Models
{
    public class EmployeeCreateRequest
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public Role Role { get; set; }
    }
}
