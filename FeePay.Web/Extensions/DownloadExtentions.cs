using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities;
using FeePay.Core.Application.IoC;
using System.IO;
using System.IO.Compression;

namespace FeePay.Web.Extensions
{
    public static class DownloadExtentions
    {
        public static byte[] GetZipPackage(this List<string> fileNames, string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));
            if (fileNames == null || !fileNames.Any()) throw new ArgumentNullException(nameof(fileNames));
            List<ArchiveFile> fileList = new List<ArchiveFile>();
            foreach (string file in fileNames)
            {
                fileList.Add(GetFile($"{filePath}{file.GetFileNameWithExtentionFromFileUrl()}"));
            }
            return GeneratePackage(fileList);
        }

        private static ArchiveFile GetFile(string filePath)
        {
            return new ArchiveFile
            {
                Name = Path.GetFileNameWithoutExtension(filePath),
                Extension = Path.GetExtension(filePath).Replace(".", ""),
                FileBytes = File.ReadAllBytes(filePath)
            };
        }

        private static byte[] GeneratePackage(List<ArchiveFile> fileList)
        {
            byte[] result;
            using (MemoryStream packageStream = new MemoryStream())
            {
                using (ZipArchive archive = new ZipArchive(packageStream, ZipArchiveMode.Create, true))
                {
                    foreach (ArchiveFile virtualFile in fileList)
                    {
                        //Create a zip entry for each attachment
                        ZipArchiveEntry zipFile = archive.CreateEntry(virtualFile.Name + "." + virtualFile.Extension);
                        using MemoryStream sourceFileStream = new MemoryStream(virtualFile.FileBytes);
                        using Stream zipEntryStream = zipFile.Open();
                        sourceFileStream.CopyTo(zipEntryStream);
                    }
                }
                result = packageStream.ToArray();
            }

            return result;
        }
    }
}
