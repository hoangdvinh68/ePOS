using AutoMapper;

namespace ePOS.Shared.Extensions;

public static class AutoMapperExtensions
{
    public static TTarget Map<TSource, TTarget>(TSource source)
    {
        var mapper = new AutoMapper.Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TSource, TTarget>();
        }));
        
        return mapper.Map<TSource, TTarget>(source);
    }
}