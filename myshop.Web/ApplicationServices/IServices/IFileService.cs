namespace myshop.Web.ApplicationServices.Interfaces
{
    public interface IFileService
    {
        public Task<string> UploadImgAsync(IFormFile obj);

        public Task<bool?> DeleteImg(string path);

        public Task<string> ValidateImg(IFormFile obj);
    }
}
