using System.ComponentModel.DataAnnotations.Schema;

namespace Mc2.CrudTest.Core.Domain.Entity;

[Table("Customers")]
public class CustomerEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [Column(TypeName = "CHAR(12)")] public string PhoneNumber { get; set; }

    public string Email { get; set; }
    public string AccountNumber { get; set; }
    public DateTimeOffset? DateOfBirth { get; set; }
}