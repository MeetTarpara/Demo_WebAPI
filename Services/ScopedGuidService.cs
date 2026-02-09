using DemoApi.Services.Interfaces;

namespace DemoApi.Services
{
    public class ScopedGuidService : IGuidService
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}
