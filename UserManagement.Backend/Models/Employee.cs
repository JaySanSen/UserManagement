using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Backend.Models
{
  public class Employee
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public long PhoneNumber { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime? CreatedDate { get; set; }
    // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    // public DateTime UpdatedDate { get; set; }

  }
}
