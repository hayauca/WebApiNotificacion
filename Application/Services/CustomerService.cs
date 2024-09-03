using Application.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerDto> GetCustomerAsync(string identificacion)      
        {         
            try
            {         
                var customer = await _customerRepository.GetCustomerByIdAsync(identificacion);                        
                return _mapper.Map<CustomerDto>(customer);
                
            }
            catch (Exception ex)
            {
                throw;
               
            }
        
        }

        public async Task CreateCustomerAsync(CustomerDto customerDto)
        {
                    
            try
            {
                var customer = _mapper.Map<Customer>(customerDto);
                await _customerRepository.CreateCustomerAsync(customer);
                //customerDto.Id = customer.Id; // Update DTO with new ID
            }
            catch (Exception ex)
            {

                throw;
            }


        }

       
    }
}
