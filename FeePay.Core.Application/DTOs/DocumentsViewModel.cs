using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.DTOs
{
    public class DocumentsViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public int UserId { get; set; }
        public string UserType { get; set; }
        public string Type { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FileType { get; set; }
        public string DownloadUrl { get; set; }
        public string HtmlAlt { get; set; }
        public string HtmlTitle { get; set; }
        public string Description { get; set; }
    }
}
