using AutoMapper;

namespace DucksNet.Application.Mappers;
public static class EmployeeMapper
{
    private static Lazy<IMapper> Lazy =
            new Lazy<IMapper>(() =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<EmployeeMappingProfile>();
                });
                var mapper = config.CreateMapper();
                return mapper;
            });
    public static IMapper Mapper => Lazy.Value;
}
