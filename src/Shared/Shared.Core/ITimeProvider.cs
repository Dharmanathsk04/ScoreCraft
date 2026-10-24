namespace Shared.Core;
public interface ITimeProvider
{
    DateTimeOffset TimeNow { get; }
}
