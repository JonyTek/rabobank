using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rabobank.TechnicalTest.GCOB.Dtos;
using Rabobank.TechnicalTest.GCOB.Repositories;
using Shouldly;

namespace Rabobank.TechnicalTest.GCOB.Tests.Controllers
{
    [TestClass]
    public class CustomerControllerTest
    {
        private ApplicationFactory _factory;

        [TestInitialize]
        public void Initialize()
        {
            _factory = new ApplicationFactory();
        }

        [TestMethod]
        public async Task GivenHaveACustomer_AndICallAServiceToGetTheCustomer_ThenTheCustomerIsReturned()
        {
            const int customerId = 10;
            const int addressId = 20;
            const int countryId = 30;
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

            var addressRepository = _factory.Services.GetService<IAddressRepository>();
            var countryRepository = _factory.Services.GetService<ICountryRepository>();
            var customerRepository = _factory.Services.GetService<ICustomerRepository>();

            await addressRepository.InsertAsync(addressDto);
            await countryRepository.InsertAsync(countryDto);
            await customerRepository.InsertAsync(customerDto);

            var httpClient = _factory.CreateClient();

            var httpResponse = await httpClient.GetAsync($"/customer/{customerId}");

            httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _factory.Dispose();
        }
    }
}
