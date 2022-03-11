using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using communicator.Entity.Base;


namespace communicator.Entity;

public class Save:Model{
    [ForeignKey("blog")]
    public string BlogId{set;get;}
    [ForeignKey("User")]
    public string UserId{set;get;}
    public virtual Blog? blog{set;get;}
    public virtual ApplicationUser User{set;get;}
    public Save():base(){
        
    }
}