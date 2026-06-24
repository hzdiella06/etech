namespace backend.Models;

public class JobApplication{

    public int Id { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public DateTime DateApplied { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser? User { get; set; }
    
}
