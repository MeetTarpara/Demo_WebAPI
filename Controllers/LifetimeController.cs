using Microsoft.AspNetCore.Mvc;
using DemoApi.Services;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("api/lifetimes")]
    public class LifetimeController : ControllerBase
    {
        private readonly TransientGuidService _transient;
        private readonly ScopedGuidService _scoped;
        private readonly SingletonGuidService _singleton;

        public LifetimeController(
            TransientGuidService transient,
            ScopedGuidService scoped,
            SingletonGuidService singleton)
        {
            _transient = transient;
            _scoped = scoped;
            _singleton = singleton;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Transient = _transient.Id,
                Scoped = _scoped.Id,
                Singleton = _singleton.Id
            });
        }
    }
}
