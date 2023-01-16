using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Rabobank.TechnicalTest.GCOB.Dtos;
using Rabobank.TechnicalTest.GCOB.Services;

namespace Rabobank.TechnicalTest.GCOB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(CustomerService customerService, ILogger<CustomerController> logger)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<Customer> Get([FromRoute] int id)
        {
            return await _customerService.FindById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerDto customerDto)
        {
            await _customerService.Insert(customerDto);

            return Ok();
        }
    }
}
