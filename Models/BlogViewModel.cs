using communicator.Entity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace communicator.Models;

public class BlogCreateViewModel
{
    public string Title { set; get; }
    public string Content { set; get; }
    [DataType(DataType.Upload)]
    public IFormFile? Image{set;get;}
}
public class BlogViewModel
{
    public string Id{set;get;}
    public string Title { set; get; }
    public string Content { set; get; }
    public string AuthorId {set;get;}
    public string AuthorEmail {set;get;}
    public DateTime CreatedAt { set; get; }
    public string Name{set;get;}
    public string ProfileId{set;get;}
    public long reactionCount{set;get;}=0;
    public bool liked {set;get;}=false;
}