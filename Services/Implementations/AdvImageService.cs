using Domain.Entities;
using Domain.IRepositories;
using Services.Abstraction.Dtos;
using Services.Abstraction.Interfaces;

namespace Services.Implementations;

public class AdvImageService : Service<AdvImage, AdvImageDto>, IAdvImageService
{
    public AdvImageService(IRepository<AdvImage> repository) 
        : base(repository)
    {
    }
}
