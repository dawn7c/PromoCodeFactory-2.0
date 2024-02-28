namespace PromoCodeFactory.Domain.Models.Administration
{
    public class Role : BaseEntity
    {
        public string Name {  get; set; }
        public string Description { get; set; }

        public Role(string name, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }
    }
}
