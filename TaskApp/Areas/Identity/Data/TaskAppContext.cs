using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskApp.Areas.Identity.Data;

namespace TaskApp.Data;

public class TaskAppContext : IdentityDbContext<TaskAppUser>
{
    public TaskAppContext(DbContextOptions<TaskAppContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
