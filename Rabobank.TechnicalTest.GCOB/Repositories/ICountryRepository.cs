using Rabobank.TechnicalTest.GCOB.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rabobank.TechnicalTest.GCOB.Repositories
{
    public interface ICountryRepository
    {
        Task InsertAsync(CountryDto customer);
        Task<CountryDto> GetAsync(int identity);
        Task<IEnumerable<CountryDto>> GetAllAsync();
    }
}
