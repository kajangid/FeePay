using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Application.Wrapper;
using FeePay.Core.Domain.Entities.Common;
using static FeePay.Core.Application.Enums.FileTypeEnum;

namespace FeePay.Core.Application.Services
{
    public class CommanDataServices : ICommanDataServices
    {
        /// <summary>
        /// Get Student Category
        /// </summary>
        /// <returns> DropDownItem List Of Student Category </returns>
        public List<DropDownItem> GetStudentCategoryDDLItems()
        {
            return new List<DropDownItem>()
            {
                new DropDownItem(){ Text = "General" , Value = "GENERAL"},
                new DropDownItem(){ Text = "OBC" , Value = "OBC"},
                new DropDownItem(){ Text = "ST" , Value = "ST"},
                new DropDownItem(){ Text = "SC" , Value = "SC"},
                new DropDownItem(){ Text = "Physically Challenged" , Value = "PHYSICALLY CHALLENGED"},
                new DropDownItem(){ Text = "Special" , Value = "SPECIAL"}
            };
        }

        /// <summary>
        /// Get Student Admission Type
        /// </summary>
        /// <returns> DropDownItem List Of Admission Types </returns>
        public List<DropDownItem> GetAdmissionTypeDDLItems()
        {
            return new List<DropDownItem>()
            {
                new DropDownItem(){ Text = "Regular" , Value = "REGULAR"},
                new DropDownItem(){ Text = "Private" , Value = "PRIVATE"},
                new DropDownItem(){ Text = "Ex-Student" , Value = "EX-STUDENT"},
                new DropDownItem(){ Text = "N/A" , Value = "N/A"}
            };
        }

        /// <summary>
        /// Get Religions
        /// </summary>
        /// <returns> DropDownItem List Of Religions </returns>
        public List<DropDownItem> GetReligionDDLItems()
        {
            return new List<DropDownItem>()
            {
                new DropDownItem(){ Text = "Hindu" , Value = "HINDU"},
                new DropDownItem(){ Text = "Muslim" , Value = "MUSLIM"},
                new DropDownItem(){ Text = "Sikh" , Value = "SIKH"},
                new DropDownItem(){ Text = "Isai" , Value = "ISAI"},
                new DropDownItem(){ Text = "Buddh" , Value = "BUDDH"},
                new DropDownItem(){ Text = "Jain" , Value = "JAIN"},
                new DropDownItem(){ Text = "Zoroastrian" , Value = "ZOROASTRIAN"},
                new DropDownItem(){ Text = "Juda" , Value = "JUDA"}
            };
        }

        /// <summary>
        /// Use to get file mimetype enum
        /// </summary>
        /// <param name="fileNameWithExtension"></param>
        /// <returns> file type enum </returns>
        public FileType GetFileTypeEnum(string fileNameWithExtension)
        {
            if (fileNameWithExtension.Contains('.'))
                fileNameWithExtension = fileNameWithExtension.Remove('.');
            switch (fileNameWithExtension.ToLower())
            {
                case "avi":
                    return FileType.Avi;
                case "doc":
                    return FileType.Doc;
                case "docx":
                    return FileType.DocX;
                case "jpg":
                    return FileType.Jpg;
                case "jpeg":
                    return FileType.Jpeg;
                case "mkv":
                    return FileType.Mkv;
                case "mp3":
                    return FileType.Mp3;
                case "mp4":
                    return FileType.Mp4;
                case "pdf":
                    return FileType.Pdf;
                case "png":
                    return FileType.Png;
                case "ppt":
                    return FileType.Ppt;
                case "pptx":
                    return FileType.Pptx;
                case "rar":
                    return FileType.Rar;
                case "txt":
                    return FileType.Text;
                case "wmv":
                    return FileType.Wmv;
                case "xls":
                    return FileType.Xls;
                case "xlsx":
                    return FileType.Xlsx;
                case "zip":
                    return FileType.Zip;
                default:
                    return FileType.Unkonwn;
            }
        }
    }
}
