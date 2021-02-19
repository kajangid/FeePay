using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.UseCase
{
    public class FileMimeType
    {
        public static string Text { get; } = "text/plain";
        public static string Doc { get; } = "application/msword";
        public static string DocX { get; } = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        public static string Xls { get; } = "application/vnd.ms-excel";
        public static string Xlsx { get; } = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public static string Ppt { get; } = "application/vnd.ms-powerpoint";
        public static string Pptx { get; } = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
        public static string Pdf { get; } = "application/pdf";
        public static string Jpg { get; } = "image/jpg";
        public static string Jpeg { get; } = "image/jpeg";
        public static string Png { get; } = "image/png";
        public static string Mp4 { get; } = "video/mp4";
        public static string Mkv { get; } = "video/x-matroska";
        public static string Avi { get; } = "video/x-msvideo";
        public static string Wmv { get; } = "video/x-ms-wmv";
        public static string Mp3 { get; } = "audio/mpeg";//"audio/mp3,audio/mpeg";
        public static string Rar { get; } = "application/vnd.rar";//"application/x-rar-compressed,application/octet-stream";
        public static string Zip { get; } = "application/zip";//"application/zip,application/octet-stream,application/x-zip-compressed,multipart/x-zip";
    }
}
