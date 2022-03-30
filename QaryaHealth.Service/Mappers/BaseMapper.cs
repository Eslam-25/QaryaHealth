using System;
using System.Collections.Generic;
using AutoMapper;
using QaryaHealth.Core.Entities;
using QaryaHealth.Service.Dtos;
using X.PagedList;

namespace QaryaHealth.Service.Mappers
{
    public static partial class Extension
    {
        private static IMapper baseMapper;
        private static readonly object baseMapperMapperlock = new object();
        public static IMapper BaseMapper<T, DTO>() where T : Base where DTO : BaseDto
        {
            if (baseMapper == null)
            {
                lock (baseMapperMapperlock)
                {
                    if (baseMapper == null)
                    {
                        baseMapper = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<T, DTO>()
                            .ReverseMap();

                            cfg.CreateMap<User, UserDto>().ReverseMap();

                        }).CreateMapper();
                    }
                }

            }

            return baseMapper;
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

        public static List<DTO> ToEnumerableDTO<T, DTO>(this List<T> entityList) where T : Base where DTO : BaseDto
        {
            return BaseMapper<T, DTO>().Map<List<T>, List<DTO>>(entityList);
        }

        public static List<T> ToEnumerableEntity<T, DTO>(this List<DTO> dtoList) where T : Base where DTO : BaseDto
        {
            return BaseMapper<T, DTO>().Map<List<DTO>, List<T>>(dtoList);
        }
        public static IPagedList<DTO> ToPagedListDTO<T, DTO>(this IPagedList<T> entityList) where T : Base where DTO : BaseDto
        {
            IEnumerable<DTO> dtoList = BaseMapper<T, DTO>().Map<IEnumerable<DTO>>(entityList);
            IPagedList<DTO> PagedList = new StaticPagedList<DTO>(dtoList, entityList.GetMetaData());
            return PagedList;
        }

        public static R GetValueOrDefault<R>(Func<R> expression)
        {

            try
            {
                return expression();
            }
            catch (NullReferenceException)
            {

                return default(R);
            }
        }

        public static R GetValueOrDefault<R>(Func<R> expression, R substitution)
        {

            try
            {
                return expression();
            }
            catch (NullReferenceException)
            {

                return substitution;
            }
        }

    }
}
