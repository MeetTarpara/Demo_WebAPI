using DemoApi.Services.Interfaces;

namespace DemoApi.Services
{
    public class TransientGuidService : IGuidService
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}
