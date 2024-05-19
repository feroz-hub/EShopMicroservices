using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Auth.Api.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<ApplicationUser> ApplicationsUsers { get; set; } = default!;
}