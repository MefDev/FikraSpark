using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FikraSparkCore.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace FikraSparkCore.Web.Endpoints;

public class Auth : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder group)
    {
        group.MapPost("/api/account/login", Login)
           .WithOpenApi()
           .Produces<AuthResponse>(StatusCodes.Status200OK)
           .Produces<AuthResponse>(StatusCodes.Status400BadRequest);
        group.MapPost("/api/account/register", Register);
        group.MapPost("/api/account/logout", Logout);
    }

    public async Task<IResult> Login(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        ILogger<Auth> logger,
        LoginRequest request)
    {
        var result = await signInManager.PasswordSignInAsync(
            request.Email, 
            request.Password, 
            isPersistent: false, 
            lockoutOnFailure: false);

      
        if (result.Succeeded)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            logger.LogInformation("User found: {UserId}, Email: {Email}", user?.Id, user?.Email);
            
            var token = GenerateJwtToken(user, configuration);
            logger.LogInformation("Token generated successfully for user: {UserId}", user?.Id);
            
            return Results.Ok(new AuthResponse(true, "Login successful", token));
        }
        
        logger.LogWarning("Login failed for email: {Email}", request.Email);
        return Results.BadRequest(new AuthResponse(false, "Invalid credentials"));
    }

    public async Task<IResult> Register(
        UserManager<ApplicationUser> userManager,
        Microsoft.AspNetCore.Identity.Data.RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            return Results.Ok(new AuthResponse(true, "Registration successful"));
        }

        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        return Results.BadRequest(new AuthResponse(false, $"Registration failed: {errors}"));
    }

    private string? GenerateJwtToken(ApplicationUser? user, IConfiguration configuration)
    {
        var secret = configuration["Jwt:Key"];
        var issuer = configuration["Jwt:Issuer"];
        
        if (string.IsNullOrWhiteSpace(secret) || string.IsNullOrWhiteSpace(issuer))
        {
            throw new Exception("JWT configuration is missing. Ensure Jwt:Key and Jwt:Issuer are set in configuration.");
        }
        
        if (user?.Email != null)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        return null;
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
    string? token= null
);

 
