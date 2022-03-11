using communicator.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;


namespace communicator.Entity;

public class Friend: Model{
    [ForeignKey("First_User")]
    public string First_UserId{set;get;}
    public ApplicationUser First_User{set;get;}
    [ForeignKey("Second_User")]
    public string Second_UserId{set;get;}
    public ApplicationUser Second_User{set;get;}
    public bool friend{set;get;}=false;
    public bool pending{set;get;}=true;
    public Friend():base(){
        
    }
}