using FeePay.Core.Domain.Entities.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Repository.School
{
    public interface IClassSectionRepository
    {
        Task<int> AddAsync(ClassSection classSection, string dbId = null);
        Task<int> DeleteAsync(int Id, string dbId = null);
        Task<int> DeleteAsync(int ClassId, int SectionId, string dbId = null);
        Task<Classes> FindSectionsInClassByClassIdAsync(int ClassId, string dbId = null);
        Task<Section> FindClassesInSectionBySectionIdAsync(int SectionId, string dbId = null);
        Task<IEnumerable<Classes>> GetAll_Class_SectionAsync(string dbId = null);
        Task<bool> IsSectionInClassAsync(int ClassId, int SectionId, string dbId = null);

        Task<IEnumerable<Classes>> GetAll_Class_Section_WithAddEditUserAsync(string dbId = null);
    }
}
