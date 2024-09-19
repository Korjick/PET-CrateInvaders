using System;

namespace Code.Infrastructure.Time
{
  public interface ITimeService
  {
    float DeltaTime { get; }
    float InGameTime { get; }
    DateTime UtcNow { get; }
  }
}