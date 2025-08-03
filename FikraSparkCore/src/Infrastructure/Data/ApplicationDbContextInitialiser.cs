using FikraSparkCore.Domain.Constants;
using FikraSparkCore.Infrastructure.Identity;
using FikraSparkCore.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FikraSparkCore.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            // See https://jasontaylor.dev/ef-core-database-initialisation-strategies
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole(Roles.Administrator);

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }

        // Test user for regular testing
        var testUser = new ApplicationUser { UserName = "test@example.com", Email = "test@example.com" };

        if (_userManager.Users.All(u => u.UserName != testUser.UserName))
        {
            await _userManager.CreateAsync(testUser, "Test123!");
            _logger.LogInformation("Created test user: test@example.com with password: Test123!");
        }

        // Sample ideas with votes
        if (!_context.Ideas.Any())
        {
            var sampleIdeas = new List<Idea>
            {
                new Idea(
                    "Implement Dark Mode", 
                    "Add a dark mode theme option to improve user experience in low-light environments. This would include dark backgrounds, light text, and appropriate contrast ratios for better readability.", 
                    15
                ),
                new Idea(
                    "Add Real-time Notifications", 
                    "Implement push notifications for new ideas, comments, and votes. Users should be able to customize their notification preferences and receive instant updates about activity on their ideas.", 
                    23
                ),
                new Idea(
                    "Create Idea Categories", 
                    "Organize ideas into categories like 'Feature Request', 'Bug Fix', 'UI/UX', 'Performance', etc. This will help users filter and find relevant ideas more easily.", 
                    8
                ),
                new Idea(
                    "Add Comment System", 
                    "Allow users to comment on ideas to provide feedback, ask questions, or suggest improvements. This will foster better collaboration and discussion around ideas.", 
                    31
                ),
                new Idea(
                    "Implement Idea Search", 
                    "Add a search functionality with filters to help users find specific ideas quickly. Include search by title, description, tags, and date range.", 
                    12
                ),
                new Idea(
                    "Add File Attachments", 
                    "Allow users to attach images, documents, or mockups to their ideas to better illustrate their concepts and provide more context.", 
                    19
                ),
                new Idea(
                    "Create User Profiles", 
                    "Add user profile pages showing their submitted ideas, voting history, and contribution statistics. This will encourage user engagement and recognition.", 
                    7
                ),
                new Idea(
                    "Add Idea Duplicate Detection", 
                    "Implement a system to detect and suggest similar ideas to prevent duplicates and help users find existing discussions on similar topics.", 
                    14
                ),
                new Idea(
                    "Implement Idea Status Tracking", 
                    "Add status tracking for ideas (New, Under Review, Approved, In Progress, Completed, Rejected) to keep users informed about the progress of their suggestions.", 
                    26
                ),
                new Idea(
                    "Add Export Functionality", 
                    "Allow users to export ideas and voting data in various formats (CSV, PDF, Excel) for reporting and analysis purposes.", 
                    5
                )
            };

            _context.Ideas.AddRange(sampleIdeas);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Seeded {Count} sample ideas with votes", sampleIdeas.Count);
        }
    }
}
