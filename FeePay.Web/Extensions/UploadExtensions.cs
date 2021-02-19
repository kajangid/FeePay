using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using FeePay.Core.Domain.Entities;

namespace FeePay.Web.Extensions
{
    public static class UploadExtensions
    {
        public static async Task<List<Upload>> UploadFiles(this IFormFileCollection files, string path, string url)
        {
            if (files == null) return null;

            var uploads = new List<Upload>();

            foreach (var file in files)
            {
                uploads.Add(await AddUpload(file, path, url));
            }

            return uploads;
        }
        public static async Task<Upload> UploadFiles(this IFormFile file, string path, string url)
        {
            if (file == null) return null;
            var upload = await AddUpload(file, path, url);
            return upload;
        }

        static async Task<Upload> AddUpload(IFormFile file, string path, string url)
        {
            var upload = await file.WriteFile(path, url);
            upload.UploadDate = DateTime.Now;
            return upload;
        }

        static async Task<Upload> WriteFile(this IFormFile file, string path, string url)
        {
            if (!(Directory.Exists(path))) Directory.CreateDirectory(path);

            var upload = await file.CreateUpload(path, url);

            using FileStream stream = new FileStream(upload.Path, FileMode.Create);
            await file.CopyToAsync(stream);

            return upload;
        }

        static Task<Upload> CreateUpload(this IFormFile file, string path, string url) => Task.Run(() =>
        {
            var name = file.CreateSafeName(path);

            var upload = new Upload
            {
                File = name,
                Name = file.Name,
                Path = $"{path}{name}",
                Url = $"{url}{name}"
            };

            return upload;
        });

        static string CreateSafeName(this IFormFile file, string path)
        {
            var increment = 0;
            var fileName = file.FileName.UrlEncode();
            var newName = fileName;

            while (File.Exists(path + newName))
            {
                var extension = fileName.Split('.').Last();
                newName = $"{fileName.Replace($".{extension}", "")}({++increment}).{extension}";
            }

            return newName;
        }

        private static readonly string urlPattern = "[^a-zA-Z0-9-.]";

        static string UrlEncode(this string url)
        {
            var friendlyUrl = Regex.Replace(url, @"\s", "-").ToLower();
            friendlyUrl = Regex.Replace(friendlyUrl, urlPattern, string.Empty);
            return friendlyUrl;
        }
    }
}
