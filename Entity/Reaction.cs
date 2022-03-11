using System.ComponentModel.DataAnnotations.Schema;
using communicator.Entity.Base;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace communicator.Entity;


public class Reaction:Model{
    [ForeignKey(nameof(Blog))]
    public string BlogId{set;get;}
    [ForeignKey(nameof(ApplicationUser))]
    public string UserId{set;get;}
    
    public bool IsLike{set;get;}=true;
    public Reaction() : base(){

    }
}