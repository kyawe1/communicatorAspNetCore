using communicator.Entity;

namespace communicator.Models;

public class BlogCreateViewModel
{
    public string Title { set; get; }
    public string Content { set; get; }
}
public class BlogViewModel
{
    public string Id{set;get;}
    public string Title { set; get; }
    public string Content { set; get; }
    public ApplicationUser Author {set;get;}
    public DateTime CreatedAt { set; get; }
}