using AutoMapper;

public class CommandsProfile : Profile
{
    public CommandsProfile()
    {
        // Source -> Target
        CreateMap<Platform,PlatformReadDto>();
        CreateMap<CommandCreateDto,Command>();
        CreateMap<Command,CommandReadDto>();
        CreateMap<PlatformPublishDto,Platform>()
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id));
    }
}