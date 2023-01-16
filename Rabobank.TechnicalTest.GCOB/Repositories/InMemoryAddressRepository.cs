using System;
using Rabobank.TechnicalTest.GCOB.Dtos;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Rabobank.TechnicalTest.GCOB.Repositories
{
    public class InMemoryAddressRepository : IAddressRepository
    {
        private static ConcurrentDictionary<int, AddressDto> Addresses { get; } = new ConcurrentDictionary<int, AddressDto>();
        private ILogger _logger;

        public InMemoryAddressRepository(ILogger logger)
        {
            _logger = logger;
        }

        public Task<int> GenerateIdentityAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task InsertAsync(AddressDto address)
        {
            if (Addresses.ContainsKey(address.Id))
            {
                throw new Exception(
                    $"Cannot insert address with identity '{address.Id}' " +
                    "as it already exists in the collection");
            }

            Addresses.TryAdd(address.Id, address);
            _logger.LogDebug($"New address inserted [ID:{address.Id}]. " +
                $"There are now {Addresses.Count} legal entities in the store.");
            return Task.FromResult(address);
        }

        public Task UpdateAsync(AddressDto address)
        {
            throw new System.NotImplementedException();
        }

        public Task<AddressDto> GetAsync(int identity)
        {
            _logger.LogDebug($"FindMany Addresses with identity {identity}");

            if (!Addresses.ContainsKey(identity)) throw new Exception(identity.ToString());
            _logger.LogDebug($"Found Address with identity {identity}");
            return Task.FromResult(Addresses[identity]);
        }
    }
}
