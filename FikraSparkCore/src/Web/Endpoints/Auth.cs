using System.ComponentModel.DataAnnotations;
using FikraSparkCore.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace FikraSparkCore.Web.Endpoints;

public class Auth : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder group)
    {
        group.MapPost("/api/account/login", Login);
        group.MapPost("/api/account/register", Register);
        group.MapPost("/api/account/logout", Logout);
    }

    public async Task<IResult> Login(
        SignInManager<ApplicationUser> signInManager,
        LoginRequest request)
    {
        var result = await signInManager.PasswordSignInAsync(
            request.Email, 
            request.Password, 
            isPersistent: false, 
            lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return Results.Ok(new AuthResponse(true, "Login successful", request.Email));
        }
        return Results.BadRequest(new AuthResponse(false, "Invalid credentials"));
    }

    public async Task<IResult> Register(
        UserManager<ApplicationUser> userManager,
        Microsoft.AspNetCore.Identity.Data.RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            Email = request.Email
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            return Results.Ok(new AuthResponse(true, "Registration successful", request.Email));
        }

        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        return Results.BadRequest(new AuthResponse(false, $"Registration failed: {errors}"));
    }

    public async Task<IResult> Logout(
        SignInManager<ApplicationUser> signInManager)
    {
        await signInManager.SignOutAsync();
        return Results.Ok(new AuthResponse(true, "Logged out successfully"));
    }
}

public record LoginRequest([Required] string Email, [Required] string Password);

public record RegisterRequest(
    [Required] string Email,
    [Required] [MinLength(6)] string Password
);

public record AuthResponse(
    bool Success,
    string Message,
    string? Email = null
);
