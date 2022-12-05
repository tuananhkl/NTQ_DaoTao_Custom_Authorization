using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomAuthorization.Data;

public class User
{
    [Key] public int Id { get; set; }

    [Column(TypeName = "nvarchar(150)")]
    [Required]
    [StringLength(250, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
    public string UserName { get; set; } = null!;

    [Column(TypeName = "nvarchar(250)")]
    [StringLength(250, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
    [Required]
    public string Password { get; set; } = null!;

    [Column(TypeName = "DateTime")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? DateOfBirth { get; set; }

    [Column(TypeName = "nvarchar(250)")]
    [Required]
    public string? Address { get; set; }

    [Column(TypeName = "nvarchar(250)")]
    [Required]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Range(1, 120, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int? Age { get; set; }

    [Required] public bool Gender { get; set; }

    public int GroupId { get; set; }
    public bool Status { get; set; }
}

// Id -> int
//     UserName -> Nvarchar(150) Not null,
// Password -> Nvarchar(250) not null,
// BirthDay -> DateTime (null)
// Address -> Nvarchar(250) (null)
//     Email -> Nvarchar(250) (null)
//     Age -> Int (null)
// Gender -> bit 