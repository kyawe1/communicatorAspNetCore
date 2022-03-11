using communicator.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace communicator.Entity;

public class Blog : Model{
    public string? Title{set;get;}
    public string? Content{set;get;}
    [ForeignKey("User")]
    public string UserId{set;get;}
    public virtual ApplicationUser User{set;get;}
    public DateTime CreaetedAt{set;get;}=DateTime.Now;
    public DateTime UpdatedAt{set;get;}=DateTime.Now;
    public Blog():base(){

    }
}

