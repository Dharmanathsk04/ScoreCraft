using Shared.Core;

namespace Shared.Infrastructure;
internal class TimeProvider : ITimeProvider
{
    public DateTimeOffset TimeNow => DateTimeOffset.UtcNow;
}
