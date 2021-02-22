using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WhatEgineerSqlite.Entity.ShopInfo;
using WhatEngineerBll.Manager;

namespace WhatEngineer.Controllers
{
    public class ManagerController : Controller
    {
        private async Task<bool> UploadFile(IFormFile ufile)
        {
            if (ufile != null && ufile.Length > 0)
            {
                var fileName = Path.GetFileName(ufile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);
                using (var fileSrteam = new FileStream(filePath, FileMode.Create))
                {
                    await ufile.CopyToAsync(fileSrteam);
                }
                return true;
            }
            return false;
        }

        public IActionResult Index()
        {
            var manage = new ManagerBll();
            var list = manage.GetAllPictureInfo();
            return View("Manager", list);
        }
        [HttpPost]
        public async Task<IActionResult> AddInstance(IEnumerable<IFormFile> files)
        {
            var dbList = new List<PictureResourceInfo>();
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\ImgSavePath", formFile.FileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    dbList.Add(new PictureResourceInfo()
                    {
                        SavePath = filePath,
                        DispPath = $"/ImgSavePath/{formFile.FileName}",
                        Descrition = $"存储位置{filePath}",
                        GId = Guid.NewGuid(),
                        CreateTime = DateTime.Now,
                        CreateBy = $"Test"
                    }) ;
                }
            }
            var manage = new ManagerBll();
            manage.AddPictureInfo(dbList);
            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            // return Ok(new { count = files.Count, size });

            var list = manage.GetAllPictureInfo();
            return View("Manager", list);
        }
    }
}
