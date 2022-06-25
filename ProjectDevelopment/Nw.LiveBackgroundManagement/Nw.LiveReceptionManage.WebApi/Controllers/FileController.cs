
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.Common.Api;
using Microsoft.AspNetCore.Authorization;
using Nw.LiveBackgroundManagement.Common;
using Microsoft.Extensions.Logging;

namespace Nw.LiveReceptionManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FileController : ControllerBase
    {
        private readonly IConfiguration _IConfiguration = null;
        private readonly ILogger<FileController> _logger;
        public FileController(
            IConfiguration iConfiguration,
            ILogger<FileController> logger
            )
        {
            this._IConfiguration = iConfiguration;
            _logger = logger;
        }

        [HttpPost]
        [Route("UploadFile")]
        public async Task<IActionResult> UploadFile()
        {
            IFormFileCollection files = Request.Form.Files;
            string resultPath = string.Empty;
            if (files != null && files.Count != 0)
            {
                IFormFile file = files.FirstOrDefault();

                //获取配置文件的文件地址
                var fileAddressSection = _IConfiguration.GetSection("FileAddress");
                if (fileAddressSection == null)
                {
                    return new JsonResult(new ApiResult()
                    {
                        ErrorMessage = "没有配置文件保存地址"
                    });
                }

                string filePath = $"{fileAddressSection.Value}/{DateTime.Now.ToString("yyyyMMdd")}";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                string suffix = string.Empty;  //后缀名
                string[] array = file.FileName.Split('.');
                if (array != null && array.Length > 0)
                {
                    suffix = array[array.Length - 1];
                }
                resultPath = Path.Combine(filePath, $"{Guid.NewGuid()}.{suffix}");
                _logger.LogWarning($"上传图片路径拼装前:{filePath}");
                _logger.LogWarning($"上传图片路径拼装后:{resultPath}");
                using (var stream = new FileStream(resultPath.TrimStart('/'), FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            return new JsonResult(new ApiResult()
            {
                Data = resultPath
            });
        }


        //[HttpPost]
        //[Route("ImgResource")]
        //public async Task<IActionResult> ImgResource(string path,string date,string contentType)
        //{
        //    //path = Path.Combine($"UploadFile/{path}", $"{Guid.NewGuid()}.{suffix}");
        //    //FileInfo fi = new FileInfo(path);
        //    //if (!fi.Exists)
        //    //{
        //    //    return null;
        //    //} 
        //    //FileStream fs = fi.OpenRead();
        //    //byte[] buffer = new byte[fi.Length]; 
        //    //fs.Read(buffer, 0, Convert.ToInt32(fi.Length));
        //    //var resource = File(buffer, "image/jpeg");
        //    //fs.Close();
        //    //return resource; 
        //}
    }
}
