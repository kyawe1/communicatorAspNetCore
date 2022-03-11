using Microsoft.AspNetCore.Identity;
namespace communicator.Entity;


public class ApplicationUser:IdentityUser<string>{
    public string? FullName{set;get;}

    public ApplicationUser(){
        this.Id=Guid.NewGuid().ToString();
    }
}