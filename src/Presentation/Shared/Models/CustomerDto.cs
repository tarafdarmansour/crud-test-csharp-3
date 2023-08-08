namespace Mc2.CrudTest.Presentation.Shared.Models
{
    public record CustomerDto
    {
        private string _phoneNumber;
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PhoneNumber
        {
            get
            {
                return $"+{_phoneNumber}";
            }
            set
            {
                _phoneNumber = value;
            }
        }

        public string Email { get; set; }
        public string AccountNumber { get; set; }
        public string? DateOfBirth { get; set; }
    }
}
