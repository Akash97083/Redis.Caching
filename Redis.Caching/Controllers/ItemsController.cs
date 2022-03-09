using Microsoft.AspNetCore.Mvc;
using Redis.Caching.Cached;
using Redis.Caching.Services;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Redis.Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsService _repository;

        public ItemsController(IItemsService itemsService)
        {
            _repository = itemsService;
        }

        // GET: api/<DashboardController>
        [HttpGet]
        [Cached(600)]
        public async Task<IActionResult> GetItemsAsync()
        {
            var items = (await _repository.GetItemsAsync());
            return Ok(items);
        }

        // GET api/<DashboardController>/5
        [HttpGet("{id}")]
        [Cached(600)]
        public async Task<IActionResult> Get(Guid id)
        {
            var item = await _repository.GetItemAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // POST api/<DashboardController>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }
    }
}
