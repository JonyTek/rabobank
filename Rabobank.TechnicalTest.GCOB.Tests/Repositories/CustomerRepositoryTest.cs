using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.Extensions.Logging;
using Rabobank.TechnicalTest.GCOB.Dtos;
using Rabobank.TechnicalTest.GCOB.Repositories;
using Shouldly;

namespace Rabobank.TechnicalTest.GCOB.Tests.Repositories
{
    [TestClass]
    public class CustomerRepositoryTest
    {
        private Mock<ILogger> _logger;
        private InMemoryCustomerRepository _customerRepository;

        [TestInitialize]
        public void Initialize()
        {
            _logger = new Mock<ILogger>();
            _customerRepository = new InMemoryCustomerRepository(_logger.Object);
        }

        [TestMethod]
        public async Task GivenHaveACustomer_AndIGetTheCustomerFromTheDB_ThenTheCustomerIsRetrieved()
        {
            const int customerId = 1;
            const int addressId = 2;
            var customerDto = new CustomerDto
            {
                Id = customerId,
                AddressId = addressId,
                FirstName = "Jonathan",
                LastName = "Swieboda"
            };
            await _customerRepository.InsertAsync(customerDto);

            var retrievedCustomerDto = await _customerRepository.GetAsync(customerId);
            retrievedCustomerDto.ShouldNotBeNull();
            retrievedCustomerDto.Id.ShouldBe(customerId);
            retrievedCustomerDto.AddressId.ShouldBe(addressId);
            retrievedCustomerDto.FirstName.ShouldBe(customerDto.FirstName);
            retrievedCustomerDto.LastName.ShouldBe(customerDto.LastName);
        }
    }
}
