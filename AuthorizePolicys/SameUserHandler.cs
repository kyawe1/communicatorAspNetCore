using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using communicator.Entity;
using System.Security.Claims;
using System.Diagnostics;

namespace communicator.AuthorizePolicys;

public class SameUserHandler : AuthorizationHandler<SameUserRequirement,Blog>
{
    // private UserManager<ApplicationUser> userManager;
    // public SameUserPolicy(UserManager<ApplicationUser> userManager)
    // {
    //     this.userManager=userManager;
    // }
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameUserRequirement requirement,Blog resource)
    {
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}