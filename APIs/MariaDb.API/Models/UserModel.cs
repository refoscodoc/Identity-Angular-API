using System.ComponentModel.DataAnnotations;

namespace MariaDb.API.Models;

public class UserModel
{
    [Key]
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public short Age { get; set; }
    public string Address { get; set; }
}