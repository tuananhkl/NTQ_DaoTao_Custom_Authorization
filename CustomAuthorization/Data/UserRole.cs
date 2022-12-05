using System.ComponentModel.DataAnnotations;

namespace CustomAuthorization.Data;

public class UserRole
{
    [Key] public int Id { get; set; }
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public bool Status { get; set; }
}