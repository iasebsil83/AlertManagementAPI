using Microsoft.EntityFrameworkCore;

namespace AlertManagementAPI.Models;

public class AlertContext : DbContext {

    public AlertContext(DbContextOptions<AlertContext> options) : base(options) {
    }

    public DbSet<Alert> Alerts { get; set; } = null!;
}
