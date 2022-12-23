using AutoMapper;
using Domain.Entities;
using Services.Abstraction.Dtos;

namespace Services.Abstraction.Mappers;

public static partial class Extension
{
    private static IMapper? baseMapper;
    private static readonly object baseMapperMapperlock = new object();
    public static IMapper BaseMapper<T, Dto>() where T : Base where Dto : BaseDto
    {
        if (baseMapper == null)
        {
            lock (baseMapperMapperlock)
            {
                if (baseMapper == null)
                {
                    baseMapper = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<T, Dto>().ReverseMap();

                        cfg.CreateMap<User, UserDto>().ReverseMap();

                        cfg.CreateMap<DonorDto, Donor>()
                            .ForPath(d => d.User, opt =>
                            {
                                opt.MapFrom(src => GetUser(src));
                                opt.Condition(con => GetUser(con.Source) != null);
                            });

                        cfg.CreateMap<Donor, DonorDto>()
                            .ForMember(r => r.Name, opt => opt.MapFrom(src => src.User.Name))
                            .ForMember(r => r.Role, opt => opt.MapFrom(src => src.User.Role))
                            .ForMember(r => r.Address, opt => opt.MapFrom(src => src.User.Address))
                            .ForMember(r => r.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                            .ForMember(r => r.Email, opt => opt.MapFrom(src => src.User.Email))
                            .ForMember(r => r.Password, opt => opt.MapFrom(src => src.User.Password));

                        cfg.CreateMap<MedicalStaffDto, MedicalStaff>()
                           .ForMember(r => r.MedicalStaffServices, opt => opt.MapFrom(src => src.Services))
                           .ForPath(d => d.User, opt =>
                           {
                               opt.MapFrom(src => GetUser(src));
                               opt.Condition(con => GetUser(con.Source) != null);
                           });

                        cfg.CreateMap<MedicalStaff, MedicalStaffDto>()
                            .ForMember(r => r.Name, opt => opt.MapFrom(src => src.User.Name))
                            .ForMember(r => r.Role, opt => opt.MapFrom(src => src.User.Role))
                            .ForMember(r => r.Address, opt => opt.MapFrom(src => src.User.Address))
                            .ForMember(r => r.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                            .ForMember(r => r.Email, opt => opt.MapFrom(src => src.User.Email))
                            .ForMember(r => r.Password, opt => opt.MapFrom(src => src.User.Password))
                            .ForMember(r => r.Services, opt => opt.MapFrom(src => src.MedicalStaffServices));

                        cfg.CreateMap<MedicalStaffServiceType, MedicalStaffServiceTypeDto>().ReverseMap();
                        cfg.CreateMap<AdvImage, AdvImageDto>().ReverseMap();

                    }).CreateMapper();
                }
            }

        }

        return baseMapper;
    }

    private static User GetUser(DonorDto donorDto)
    {
        return new User
        {
            Id = donorDto.UserId,
            Name = donorDto.Name,
            IsActive = donorDto.IsActive,
            Address = donorDto.Address,
            Password = donorDto.Password,
            PhoneNumber = donorDto.PhoneNumber,
            Email = donorDto.Email,
            Role = donorDto.Role
        };
    }
    private static User GetUser(MedicalStaffDto donorDto)
    {
        return new User
        {
            Id = donorDto.UserId,
            Name = donorDto.Name,
            IsActive = donorDto.IsActive,
            Address = donorDto.Address,
            Password = donorDto.Password,
            PhoneNumber = donorDto.PhoneNumber,
            Email = donorDto.Email,
            Role = donorDto.Role
        };
    }

    public static DTO ToDTO<T, DTO>(this T entity) where T : Base where DTO : BaseDto
    {
        return BaseMapper<T, DTO>().Map<T, DTO>(entity);
    }

    public static T ToEntity<T, DTO>(this DTO dto) where T : Base where DTO : BaseDto
    {
        return BaseMapper<T, DTO>().Map<DTO, T>(dto);
    }

    public static T ToEntity<T, DTO>(this DTO dto, T entity) where T : Base where DTO : BaseDto
    {
        return BaseMapper<T, DTO>().Map(dto, entity);
    }

    public static IEnumerable<DTO> ToEnumerableDTO<T, DTO>(this IEnumerable<T> entityList) where T : Base where DTO : BaseDto
    {
        return BaseMapper<T, DTO>().Map<IEnumerable<T>, IEnumerable<DTO>>(entityList);
    }

    public static IEnumerable<T> ToEnumerableEntity<T, DTO>(this IEnumerable<DTO> dtoList) where T : Base where DTO : BaseDto
    {
        return BaseMapper<T, DTO>().Map<IEnumerable<DTO>, IEnumerable<T>>(dtoList);
    }

    public static List<DTO> ToListDTO<T, DTO>(this List<T> entityList) where T : Base where DTO : BaseDto
    {
        return BaseMapper<T, DTO>().Map<List<T>, List<DTO>>(entityList);
    }

    public static List<T> ToListEntity<T, DTO>(this List<DTO> dtoList) where T : Base where DTO : BaseDto
    {
        return BaseMapper<T, DTO>().Map<List<DTO>, List<T>>(dtoList);
    }
}
