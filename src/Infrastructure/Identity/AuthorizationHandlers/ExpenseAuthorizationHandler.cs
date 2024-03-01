using Azure;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata;
using Organizer.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

public record AccessExpenseAuthorizationRequirement() : IAuthorizationRequirement;
public class ExpenseAuthorizationHandler :
    AuthorizationHandler<AccessExpenseAuthorizationRequirement, Object>
{
    private readonly IApplicationDbContext _context;

    public ExpenseAuthorizationHandler(IApplicationDbContext context) : base()
    {
        _context = context;
    }


    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   AccessExpenseAuthorizationRequirement requirement,
                                                   Object resource)
    {
        Guard.Against.Null(context.User);
        Guard.Against.Null(context.User.Identity);
        Guard.Against.Null(context.User.Identity.Name);

        
        context.Succeed(requirement);

        return Task.CompletedTask;
    }

}
