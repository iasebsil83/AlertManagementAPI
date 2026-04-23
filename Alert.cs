using System;

namespace AlertManagementAPI;

public enum STATUS {
    DRAFT,
    PUBLISHED,
    CANCELLED
}

public class Alert {
    public       int ID        { get; set; }

    public   string? Message   { get; set; }

    public    STATUS Status    { get; set; }

    public   string? Area      { get; set; }

    public DateOnly? CreatedAt { get; set; }
}
