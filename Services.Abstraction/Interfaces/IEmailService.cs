using Services.Abstraction.Models;

namespace Services.Abstraction.Interfaces;

public interface IEmailService
{
    bool SendAsync(PayloadModel payload);
}
