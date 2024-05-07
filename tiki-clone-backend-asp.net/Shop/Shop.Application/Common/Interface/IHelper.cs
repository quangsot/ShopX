using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Common
{
    public interface IHelper
    {
        /// <summary>
        /// lưu ảnh
        /// </summary>
        /// <param name="image"></param>
        /// <returns>đường dẫn của ảnh</returns>
        public Task<string> SaveImageAsync(IFormFile? image);

        public Task<IFormFile> GetImageAsync(string path);
        
        /// <summary>
        /// Xóa ảnh theo path
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool DeleteImage(string filePath);
    }
}
