using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Moq;
using Rabobank.TechnicalTest.GCOB.Dtos;
using Rabobank.TechnicalTest.GCOB.Repositories;
using Rabobank.TechnicalTest.GCOB.Services;
using Shouldly;

namespace Rabobank.TechnicalTest.GCOB.Tests.Services
{
    [TestClass]
    public class CustomerServiceTest
    {
        private CustomerService _customerService;
        private Mock<IAddressRepository> _addressRepository;
        private Mock<ICountryRepository> _countryRepository;
        private Mock<ICustomerRepository> _customerRepository;

        [TestInitialize]
        public void Initialize()
        {
            _addressRepository = new Mock<IAddressRepository>();
            _countryRepository = new Mock<ICountryRepository>();
            _customerRepository = new Mock<ICustomerRepository>();
            _customerService = new CustomerService(_addressRepository.Object, _countryRepository.Object, _customerRepository.Object);
        }

        [TestMethod]
        public async Task GivenHaveACustomer_AndICallAServiceToGetTheCustomer_ThenTheCustomerIsReturned()
        {
            const int customerId = 1;
            const int addressId = 2;
            const int countryId = 3;
            var addressDto = new AddressDto
            {
                Id = addressId,
                City = "London",
                Postcode = "E8 4DT",
                Street = "Fake Street",
                CountryId = countryId
            };
            var countryDto = new CountryDto
            {
                Id = countryId,
                Name = "England"
            };
            var customerDto = new CustomerDto
            {
                Id = customerId,
                AddressId = addressId,
                FirstName = "Jonathan",
                LastName = "Swieboda"
            };
            _addressRepository
                .Setup(x => x.GetAsync(addressId))
                .ReturnsAsync(addressDto);
            _countryRepository
                .Setup(x => x.GetAsync(countryId))
                .ReturnsAsync(countryDto);
            _customerRepository
                .Setup(x => x.GetAsync(customerId))
                .ReturnsAsync(customerDto);

            var customer = await _customerService.FindById(customerId);

            customer.Id.ShouldBe(customerId);
            customer.City.ShouldBe(addressDto.City);
            customer.Postcode.ShouldBe(addressDto.Postcode);
            customer.Street.ShouldBe(addressDto.Street);
            customer.Country.ShouldBe(countryDto.Name);
            customer.FullName.ShouldBe($"{customerDto.FirstName} {customerDto.LastName}");
        }

        [TestMethod]
        public async Task GivenInsertACustomer_AndICallAServiceToGetTheCustomer_ThenTheCustomerIsInserted_AndTheCustomerIsReturned()
        {
            const int customerId = 1;
            const int addressId = 2;
            const int countryId = 3;
            var addressDto = new AddressDto
            {
                Id = addressId,
                City = "London",
                Postcode = "E8 4DT",
                Street = "Fake Street",
                CountryId = countryId
            };
            var countryDto = new CountryDto
            {
                Id = countryId,
                Name = "England"
            };
            var customerDto = new CustomerDto
            {
                Id = countryId,
                AddressId = addressId,
                FirstName = "Jonathan",
                LastName = "Swieboda"
            };
            _addressRepository
                .Setup(x => x.GetAsync(addressId))
                .ReturnsAsync(addressDto);
            _countryRepository
                .Setup(x => x.GetAsync(countryId))
                .ReturnsAsync(countryDto);
            _customerRepository
                .Setup(x => x.GetAsync(customerId))
                .ReturnsAsync(customerDto);

            await _customerService.Insert(customerDto);
            _customerRepository.Verify(x => x.InsertAsync(customerDto), Times.Once);

            var customer = await _customerService.FindById(customerId);
            customer.Id.ShouldBe(customerId);
            customer.City.ShouldBe(addressDto.City);
            customer.Postcode.ShouldBe(addressDto.Postcode);
            customer.Street.ShouldBe(addressDto.Street);
            customer.Country.ShouldBe(countryDto.Name);
            customer.FullName.ShouldBe($"{customerDto.FirstName} {customerDto.LastName}");
        }
    }
}
