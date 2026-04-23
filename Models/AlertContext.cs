using Microsoft.EntityFrameworkCore;

namespace AlertManagementAPI.Models;

public class AlertContext : DbContext {

    //custom instance for alerts
    public AlertContext(DbContextOptions<AlertContext> options) : base(options) {
    }

    //alert list
    public DbSet<Alert> Alerts { get; set; } = null!;
}
