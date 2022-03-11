using System.ComponentModel.DataAnnotations;


namespace communicator.Models;

public class ProfileViewModel
{
    public string DisplayName { set; get; }
    public string address { set; get; }
    public string Email {set;get;}
    public string? PhoneNumber{set;get;}
    public string? UserId{set;get;}
    // public string Sex{set;get;}
    public DateTime? Date_Of_Birth { set; get; }
}
public class ProfileCreateViewModel
{
    public string DisplayName { set; get; }
    public string address { set; get; }
    [DataType(DataType.DateTime)]
    public DateTime Date_Of_Birth { set; get; }
}