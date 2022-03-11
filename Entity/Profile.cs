using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using communicator.Entity.Base;

namespace communicator.Entity;

public class Profile :Model{
    [DataType(DataType.DateTime)]
    public DateTime? Date_Of_Birth{set;get;}
    public string? DisplayName{set;get;}
    public ApplicationUser? User{set;get;}
    [ForeignKey("User")]
    public string? UserId{set;get;}
    public string? address{set;get;}

    public Profile():base(){
        
    }
}