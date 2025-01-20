namespace UserManagement.Backend.DTOs
{
  public class EmployeeDTO
  {
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public long PhoneNumber { get; set; }

  }
}
