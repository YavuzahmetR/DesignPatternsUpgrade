using System.IO.Compression;

namespace WebApp.ChainOfResponsibility.ChainOfRes
{
    public class ZipFileProcessHandler<T> : ProcessHandler
    {
        public override object Handle(object request)
        {
            var excelMemoryStream = request as MemoryStream;

            excelMemoryStream!.Position = 0;

            using (var zipStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                {
                    var zipArchiveEntry = archive.CreateEntry($"{typeof(T).Name}.xlsx");
                    using (var entryStream = zipArchiveEntry.Open())
                    {
                        excelMemoryStream.CopyTo(entryStream);
                    }
                }
                return base.Handle(zipStream);
            }
        }
    }
}
