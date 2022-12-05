using System.ComponentModel.DataAnnotations;

namespace CustomAuthorization.Data;

public class Role
{
    // ID
    //     RoleName(Nvarchar(250))
    // Action (varchar(150)) Tên action
    // Controller varchar(150) Tên controller
    [Key]
    public int Id { get; set; }

    public string Action { get; set; } = null!;

    public string Controller { get; set; } = null!;
}