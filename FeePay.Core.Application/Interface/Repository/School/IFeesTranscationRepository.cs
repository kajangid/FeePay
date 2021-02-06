using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities.School;
using FeePay.Core.Domain.Entities.Student;

namespace FeePay.Core.Application.Interface.Repository.School
{
    public interface IFeesTranscationRepository
    {
        Task<int> AddAsync(FeesTranscation feesTranscation, List<StudentFees> fees, string dbId);
        Task<int> UpdateAsync(FeesTranscation feesTranscation, List<StudentFees> fees, string dbId);
        Task<FeesTranscation> FindByIdAsync(int feeTranscationId, string dbId);
        Task<FeesTranscation> FindByTranscationIdAsync(string transcationId, string dbId);
    }
}
