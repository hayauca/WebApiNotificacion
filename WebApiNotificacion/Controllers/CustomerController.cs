using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApiNotificacion.Controllers
{
    
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

   
        [HttpGet("getcustomer")]
        public async Task<IActionResult> GetCustomer(string identificacion)
        {

            try
            {              
                var customer = await _customerService.GetCustomerAsync(identificacion);
                if (customer == null)
                {
                    return NotFound();
                }

                return Ok(customer);
            }      
            catch (Exception ex)
            {
                throw;
                
            }
                                    
        }
     

        [HttpPost]
        [Route("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer(CustomerDto customerDto)
        {
            try
            {
                await _customerService.CreateCustomerAsync(customerDto);
                return CreatedAtAction(nameof(GetCustomer), new { id = customerDto.lc_emisor }, customerDto);
            }
            catch (Exception ex)
            {

                throw; 
            }
            
            
        }
    }
}
