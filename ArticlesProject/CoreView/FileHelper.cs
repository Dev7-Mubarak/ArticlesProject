namespace ArticlesProject.CoreView
{
    public class FileHelper
    {
        private readonly IWebHostEnvironment _webHost;
        public FileHelper(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }
        public string UploadFile(IFormFile file, string folder)
        {
            if (file != null)
            {
                var firDir = Path.Combine(_webHost.WebRootPath, folder);
                var fileName = Guid.NewGuid() + "-" + file.FileName;
                var filePath = Path.Combine(firDir, fileName);

                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                    return fileName;
                }
            }

            return string.Empty;
        }
    }
}