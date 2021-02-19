using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities.School;

namespace FeePay.Core.Application.Interface.Repository.School
{
    public interface IClassRepository
    {
        #region Execute
        /// <summary>
        /// Add New Class
        /// </summary>
        /// <param name="classes"> Class Object To Update </param>
        /// <param name="dbId"> School Db Key </param>
        /// <returns> Id Of Added Class If 0 Class Is Already Exists. </returns>
        Task<int> AddAsync(Classes classes, string dbId);

        /// <summary>
        /// Update Class
        /// </summary>
        /// <param name="classes"> Class Object To Update </param>
        /// <param name="dbId"> School Db Key </param>
        /// <returns> Id Of Updated Class If 0 Class Is Not Already. </returns>
        Task<int> UpdateAsync(Classes classes, string dbId);

        /// <summary>
        /// Delete Class
        /// </summary>
        /// <param name="id"> Id Of The Class </param>
        /// <param name="dbId"> School Db Key </param>
        /// <returns> Number Of Row Effected </returns>
        Task<int> DeleteAsync(int id, string dbId);
        #endregion

        #region Find
        /// <summary>
        /// Fetch the Class Data
        /// </summary>
        /// <param name="id"> Id Of The Class </param>
        /// <param name="dbId"> School Db Key </param>
        /// <param name="isActive"> Class Active Condition </param>
        /// <returns> Class Object Data </returns>
        Task<Classes> FindByIdAsync(int id, string dbId, bool? isActive = null);

        /// <summary>
        /// Fetch the Class Data
        /// </summary>
        /// <param name="name"> Name Of The Class </param>
        /// <param name="dbId"> School Db Key </param>
        /// <param name="isActive"> Class Active Condition </param>
        /// <returns> Class Object Data </returns>
        Task<Classes> FindByNameAsync(string name, string dbId, bool? isActive = null);
        #endregion

        #region Get All
        /// <summary>
        /// Get All Classes
        /// </summary>
        /// <param name="dbId"> School Db Key </param>
        /// <param name="isActive"> Class Active Condition </param>
        /// <returns> IEnumerable Classes Object </returns>
        Task<IEnumerable<Classes>> GetAllAsync(string dbId, bool? isActive = null);

        /// <summary>
        /// Get All Classes with Audit data
        /// </summary>
        /// <param name="dbId"> School Db Key </param>
        /// <param name="isActive"> Class Active Condition </param>
        /// <returns> IEnumerable Classes Object </returns>
        Task<IEnumerable<Classes>> GetAll_WithAddEditUserAsync(string dbId, bool? isActive = null);
        #endregion


    }
}
