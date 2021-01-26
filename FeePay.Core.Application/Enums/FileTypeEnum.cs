using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Enums
{
    public class FileTypeEnum
    {
        public enum FileType
        {

            /// <summary>
            /// Select if you want <see cref="IFormFile"/> should be a text file.
            /// </summary>
            [Description("text/plain")]
            Text,

            /// <summary>
            /// Select if you want <see cref="IFormFile"/> should be a ms-word document file.
            /// </summary>
            [Description("application/msword")]
            Doc,

            /// <summary>
            /// Select if you want <see cref="IFormFile"/> should be a ms-word document file.
            /// </summary>
            [Description("application/vnd.openxmlformats-officedocument.wordprocessingml.document")]
            DocX,

            /// <summary>
            /// Select if you want <see cref="IFormFile"/> should be an excel file.
            /// </summary>
            [Description("application/vnd.ms-excel")]
            Xls,

            /// <summary>
            /// Select if you want <see cref="IFormFile"/> should be an excel file.
            /// </summary>
            [Description("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
            Xlsx,

            /// <summary>
            /// Select if you want <see cref="IFormFile"/> should be a power point file.
            /// </summary>
            [Description("application/vnd.ms-powerpoint")]
            Ppt,

            /// <summary>
            /// Select if you want <see cref="IFormFile"/> should be a power point file.
            /// </summary>
            [Description("application/vnd.openxmlformats-officedocument.presentationml.presentation")]
            Pptx,

            /// <summary>
            /// Select if you want <see cref="IFormFile"/> should be a pdf file.
            /// </summary>
            [Description("application/pdf")]
            Pdf,

            /// <summary>
            /// Select if you want <see cref="IFormFile"/> should be a jpg file.
            /// </summary>
            [Description("image/jpg")]
            Jpg,

            /// <summary>
            /// Select if you want <see cref="IFormFile"/> should be a jpeg file.
            /// </summary>
            [Description("image/jpeg")]
            Jpeg,

            /// <summary>
            /// Select if you want <see cref="IFormFile"/> should be a png file.
            /// </summary>
            [Description("image/png")]
            Png,

            /// <summary>
            /// Select if you want <see cref="IFormFile"/> should be a mp4 file.
            /// </summary>
            [Description("video/mp4")]
            Mp4,

            /// <summary>
            /// Select if you want <see cref="IFormFile"/> should be a mkv file.
            /// </summary>
            [Description("video/x-matroska")]
            Mkv,

            /// <summary>
            /// Select if you want <see cref="IFormFile"/> should be a avi file.
            /// </summary>
            [Description("video/x-msvideo")]
            Avi,

            /// <summary>
            /// Select if you want <see cref="IFormFile"/> should be a wmv file.
            /// </summary>
            [Description("video/x-ms-wmv")]
            Wmv,

            /// <summary>
            /// Select if you want <see cref="IFormFile"/> should be a mp3 file.
            /// </summary>
            [Description("audio/mp3,audio/mpeg")]
            Mp3,

            /// <summary>
            /// Select if you want <see cref="IFormFile"/> should be a rar file.
            /// </summary>
            [Description("application/x-rar-compressed,application/octet-stream")]
            Rar,

            /// <summary>
            /// Select if you want <see cref="IFormFile"/> should be a zip file.
            /// </summary>
            [Description("application/zip,application/octet-stream,application/x-zip-compressed,multipart/x-zip")]
            Zip
        }
    }
}
