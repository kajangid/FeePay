using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities;

namespace FeePay.Core.Application.Interface.Repository.School
{
    public interface ICityStateRepository
    {
        Task<States> FindActiveStateByIdAsync(int id, string dbId = null);
        Task<Cities> FindActiveCityByIdAsync(int id, string dbId = null);
        Task<IEnumerable<Cities>> FindActiveCitiesByStateIdAsync(int id, string dbId = null);
        Task<IEnumerable<States>> GetAllActiveStatesAsync(string dbId = null);
        Task<IEnumerable<Cities>> GetAllActiveCitiesAsync(string dbId = null);
    }
}
