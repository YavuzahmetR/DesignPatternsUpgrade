﻿using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using WebApp.Command.Commands.WebApp.Command.Commands;
using WebApp.Command.Commands;
using WebApp.Command.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Command.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IdentityContext _context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ProductsController(IdentityContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        public async Task<IActionResult> CreateFile(int type)
        {
            var products = await _context.Products.ToListAsync();

            FileCreateInvoker fileCreateInvoker = new FileCreateInvoker();

            EFileType fileType = (EFileType)type;

            switch (fileType)
            {
                case EFileType.Excel:
                    ExcelFile<Product> excelFile = new ExcelFile<Product>(products);

                    fileCreateInvoker.SetCommand(new CreateExcelTableActionCommand<Product>(excelFile));

                    break;

                case EFileType.Pdf:
                    PdfFile<Product> pdfFile = new PdfFile<Product>(products, httpContextAccessor);
                    fileCreateInvoker.SetCommand(new CreatePdfTableActionCommand<Product>(pdfFile));
                    break;

                default:
                    break;
            }

            return fileCreateInvoker.CreateFile();
        }

        public async Task<IActionResult> CreateFiles()
        {
            var products = await _context.Products.ToListAsync();
            ExcelFile<Product> excelFile = new(products);
            PdfFile<Product> pdfFile = new(products, httpContextAccessor);
            FileCreateInvoker fileCreateInvoker = new();

            fileCreateInvoker.AddCommand(new CreateExcelTableActionCommand<Product>(excelFile));
            fileCreateInvoker.AddCommand(new CreatePdfTableActionCommand<Product>(pdfFile));

            var filesResult = fileCreateInvoker.CreateFiles();

            using (var zipMemoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(zipMemoryStream, ZipArchiveMode.Create))
                {
                    foreach (var item in filesResult)
                    {
                        var fileContent = item as FileContentResult;

                        var zipFile = archive.CreateEntry(fileContent!.FileDownloadName);

                        using (var zipEntryStream = zipFile.Open())
                        {
                            await new MemoryStream(fileContent.FileContents).CopyToAsync(zipEntryStream);
                        }
                    }
                }

                return File(zipMemoryStream.ToArray(), "application/zip", "all.zip");
            }
        }
    }
}
