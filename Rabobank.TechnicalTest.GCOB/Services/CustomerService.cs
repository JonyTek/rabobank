using System.Threading.Tasks;
using Rabobank.TechnicalTest.GCOB.Dtos;
using Rabobank.TechnicalTest.GCOB.Repositories;

namespace Rabobank.TechnicalTest.GCOB.Services
{
    public class CustomerService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(IAddressRepository addressRepository, ICountryRepository countryRepository, ICustomerRepository customerRepository)
        {
            _addressRepository = addressRepository;
            _countryRepository = countryRepository;
            _customerRepository = customerRepository;
        }
        
        public async Task Insert(CustomerDto customerDto)
        {
            await _customerRepository.InsertAsync(customerDto);
        }

        public async Task<Customer> FindById(int customerId)
        {
            var customer = await _customerRepository.GetAsync(customerId);
            var address = await _addressRepository.GetAsync(customer.AddressId);
            var country = await _countryRepository.GetAsync(address.CountryId);

            return new Customer
            {
                Id = customerId,
                City = address.City,
                Postcode = address.Postcode,
                Street = address.Street,
                Country = country.Name,
                FullName = $"{customer.FirstName} {customer.LastName}"
            };
        }
    }
}
