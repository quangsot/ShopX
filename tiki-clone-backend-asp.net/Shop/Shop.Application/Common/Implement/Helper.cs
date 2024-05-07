using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Common
{
    public class Helper : IHelper
    {
        public bool DeleteImage(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    // Xóa file
                    File.Delete(filePath);
                    return true;
                }
                catch (IOException e)
                {
                    throw;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<IFormFile> GetImageAsync(string path)
        {
            // Kiểm tra xem đường dẫn có tồn tại không
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File không tồn tại", path);
            }

            byte[] imageData;

            // Đọc dữ liệu của ảnh từ đường dẫn
            try
            {
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    // Tạo một bộ đệm để đọc dữ liệu từ tập tin
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        // Đọc dữ liệu từ tập tin vào bộ nhớ
                        await stream.CopyToAsync(memoryStream);

                        // Lấy dữ liệu hình ảnh dưới dạng mảng byte
                        imageData = memoryStream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ khi có lỗi xảy ra trong quá trình đọc tập tin
                throw new Exception("Đã xảy ra lỗi khi đọc tập tin hình ảnh", ex);
            }

            // Tạo một IFormFile giả mạo từ dữ liệu hình ảnh
            var formFile = new FormFile(new MemoryStream(imageData), 0, imageData.Length, "imageFile", "image.jpg");
            return formFile;
        }

        public async Task<string> SaveImageAsync(IFormFile? image)
        {
            if (image != null && image.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", image.FileName);
                using (var stream = File.Create(path))
                {
                    await image.CopyToAsync(stream);
                }

                return $"/Images/{image.FileName}";
            }
            else return string.Empty;
        }
    }
}
