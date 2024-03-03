using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace QuanLiNhaSach.Utils
{
    public class CloudService
    {
        private static CloudService _ins;

        public static CloudService Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new CloudService();
                return _ins;
            }
            private set
            {
                _ins = value;
            }
        }
        private Account account;
        private Cloudinary cloudinary;
        private string CLOUD_NAME = "dpd0benvr";
        private string API_KEY = "619689586522497";
        private string API_SECRET = "2R4qMR8HJ6j05TkcMkGw5JaurO0";
        public CloudService()
        {
            account = new Account(CLOUD_NAME, API_KEY, API_SECRET);
            cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;
        }
        #region methods
        string GetIDFromURI(string uri)
        {
            string id = "";
            string startString = "NhaSachUIT";
            string endString = ".";
            if (uri.Contains(startString) && uri.Contains(endString))
            {
                int start = uri.IndexOf(startString);
                int end = uri.IndexOf(endString, start);
                id = uri.Substring(start, end - start);
            }
            return id;
        }
        #endregion
        public async Task<string> UploadImage(string ImagePath)
        {
            try
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(ImagePath),
                    Folder = "NhaSachUIT"
                };
                var result = await cloudinary.UploadAsync(uploadParams);
                return result.SecureUrl.AbsoluteUri;
            }
            catch (System.Exception e)
            {
                return null;
            }
        }
        public async Task<string> DeleteImage(string uri)
        {
            try
            {
                string ID = GetIDFromURI(uri);
                var deleteParams = new DeletionParams(ID)
                {
                    ResourceType = ResourceType.Image
                };
                var result = await cloudinary.DestroyAsync(deleteParams);
                return "Đã xóa thành công!";
            }
            catch (System.Exception)
            {
                return "Có lỗi xuất hiện!";
            }

        }

    }
}
