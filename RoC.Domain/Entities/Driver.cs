namespace RoC.Domain.Entities;

public class Driver
{
  public int Id { get; set; }
  public required string FirstName { get; set; }
  public string FirstName2 { get; set; }
  public required string LastName { get; set; }
  public required string Pesel { get; set; }
  public string Email { get; set; }
  public string PhoneNumber { get; set; }
  public string NrLicenses { get; set; }
  public required DateTime DateOfBirth { get; set; }
  public bool Active { get; set; }
}