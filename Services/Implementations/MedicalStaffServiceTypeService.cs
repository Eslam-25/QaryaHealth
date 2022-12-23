using Domain.Entities;
using Domain.IRepositories;
using Services.Abstraction.Dtos;
using Services.Abstraction.Interfaces;

namespace Services.Implementations;

public class MedicalStaffServiceTypeService : Service<MedicalStaffServiceType, MedicalStaffServiceTypeDto>, IMedicalStaffServiceTypeService
{
    public MedicalStaffServiceTypeService(IRepository<MedicalStaffServiceType> repository) 
        : base(repository)
    {
    }
}
