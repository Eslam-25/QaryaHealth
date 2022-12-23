namespace Services.EmailEngine;

public class EmailConfig
{
    public required string Host { get; set; }
    public required int Port { get; set; }
    public required string SendFrom { get; set; }
    public required string Password { get; set; }
}
