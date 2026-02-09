using DemoApi.Services.Interfaces;

namespace DemoApi.Services
{
    public class SingletonGuidService : IGuidService
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}
