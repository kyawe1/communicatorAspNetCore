using System.ComponentModel.DataAnnotations;

namespace communicator.Entity.Base;

public class Model{
    [Key]
    [StringLength(36)]
    public virtual string Id{set;get;}

    public Model(){
        this.Id=Guid.NewGuid().ToString();
    }
}
