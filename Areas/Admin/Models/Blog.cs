using communicator.Entity;

namespace communicator.Areas.Admin.Models;

public class BlogViewModel{
    public string Id{set;get;}
    public string Title{set;get;}
    public string Content{set;get;}
    public ApplicationUser? User{set;get;}
    public string UserId{set;get;}
    public DateTime? CreatedAt{set;get;}=DateTime.Now;
    public DateTime? UpdatedAt{set;get;}=DateTime.Now;
}

public class CreateBlog{
    public string Title{set;get;}
    public string Content{set;get;}
    
    public string? User{set;get;}
}