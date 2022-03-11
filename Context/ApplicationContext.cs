using communicator.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace communicator.Context;

public class ApplicationContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }
    public DbSet<Blog> Blogs { set; get; }
    public DbSet<Profile> profiles { set; get; }
    public DbSet<Friend> friends { set; get; }
    public DbSet<Reaction> reactions { set; get; }
    public DbSet<Save> savedblogs {set;get;}


}

