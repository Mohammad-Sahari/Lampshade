﻿using _01_Framework.Application;

namespace ServiceHost
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string Upload(IFormFile file,string path)
        {
            if (file == null) return "";
            var directoryPath = $"{_webHostEnvironment.WebRootPath}//ProductPictures//{path}";
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var fileName = $"{DateTime.Now.ToFileTime()}-{file.FileName}";
            var filePath = $"{directoryPath}//{fileName}";
            using (var output = System.IO.File.Create(filePath))
            {
                file.CopyToAsync(output);
            }

            return $"{path}/{fileName}";
        }
    }
}
