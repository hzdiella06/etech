namespace backend.DTOs;

public class CreateJobApplicationDto{

    public string CompanyName { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public DateTime DateApplied { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }

}
