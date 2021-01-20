using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities.Identity;

namespace FeePay.Core.Domain.Entities.School
{
    public class Section : BaseEntitie
    {
        public Section()
        {
            Classes = new List<Classes>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Description { get; set; }



        public List<Classes> Classes { get; set; }

        public SchoolAdminUser AddedByUser { get; set; }
        public SchoolAdminUser ModifyByUser { get; set; }
    }
}
