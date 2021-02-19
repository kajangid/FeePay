using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Domain.Entities
{
    public class ArchiveFile
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public byte[] FileBytes { get; set; }
    }
}
