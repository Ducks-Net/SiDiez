using AutoMapper;

namespace DucksNet.Application.Mappers;
public class MedicalRecordMapper
{
    private static Lazy<IMapper> Lazy =
            new Lazy<IMapper>(() =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<MedicalRecordMappingProfile>();
                });
                var mapper = config.CreateMapper();
                return mapper;
            });
    public static IMapper Mapper => Lazy.Value;
}
