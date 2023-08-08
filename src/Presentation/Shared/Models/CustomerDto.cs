namespace Mc2.CrudTest.Presentation.Shared.Models
{
    public record CustomerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AccountNumber { get; set; }
        public string? DateOfBirth { get; set; }
    }
}
