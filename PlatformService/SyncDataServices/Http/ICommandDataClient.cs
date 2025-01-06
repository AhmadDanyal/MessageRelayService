using System.Threading.Tasks;

public interface ICommandDataClient
{
    Task SendPlatformToCommand(PlatformReadDto platform);
}