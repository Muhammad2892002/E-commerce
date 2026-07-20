using myshop.Web.ApplicationServices.Interfaces;

namespace myshop.Web.ApplicationServices.Services
{
    public class HandlingFiles : IFileService
    {
        private readonly IWebHostEnvironment _environment;
        private static string storageImgFolder = @"Images\Products\";

        public HandlingFiles(IWebHostEnvironment environment) { 
           
            _environment= environment;
        
        }
        public Task<bool?> DeleteImg(string path)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadImgAsync(IFormFile obj)
        {

          

            var wwwrootPath = _environment.WebRootPath;
            var newFileName = Guid.NewGuid().ToString();

            var storageFilePlace = Path.Combine(wwwrootPath, storageImgFolder);
            var ext = Path.GetExtension(obj.FileName);

            var fullImgpath = Path.Combine(storageFilePlace, newFileName+ext);
            await using (var fileStream = new FileStream(fullImgpath, FileMode.Create))
            {

                obj.CopyTo(fileStream);


            }
            var path=storageImgFolder + newFileName + ext;
            return path;
        }

        public Task<string> ValidateImg(IFormFile obj)
        {
            throw new NotImplementedException();
        }
    }
}
