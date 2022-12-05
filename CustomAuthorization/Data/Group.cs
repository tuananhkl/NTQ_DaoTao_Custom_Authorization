using System.ComponentModel.DataAnnotations;

namespace CustomAuthorization.Data;

public class Group
{
    [Key] public int Id { get; set; }
    public string GroupName { get; set; } = null!;
    public bool Status { get; set; }
}