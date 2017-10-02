namespace _31_boilerplate_api.Services
{
    using System;

    /// <summary>
    /// Retrieves the current time. Helps with unit testing by letting you mock the system clock.
    /// </summary>
    public class ClockService : IClockService
    {
        public DateTimeOffset UtcNow { get => DateTimeOffset.UtcNow; }
    }
}
