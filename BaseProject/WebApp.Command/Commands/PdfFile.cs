using DinkToPdf;
using DinkToPdf.Contracts;
using System.Text;

namespace WebApp.Command.Commands
{
    public class PdfFile<T>
    {
        private readonly List<T> _list;
        private readonly IHttpContextAccessor _context;

        public PdfFile(List<T> list, IHttpContextAccessor context)
        {
            _list = list;
            _context = context;
        }

        private readonly string _fileName = $"{typeof(T).Name}.pdf";
        public string FileName => _fileName;
        public string FileType => "application/octet-stream";

        public MemoryStream Create()
        {
            var type = typeof(T);
            var sb = new StringBuilder();

            // HTML Başlangıcı
            sb.Append($@"
                        <html>
                            <head>
                                <link rel='stylesheet' href='{Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/lib/bootstrap/dist/css/bootstrap.css")}' />
                            </head>
                            <body>
                                <div class='text-center'>
                                    <h1>{type.Name} Tablo</h1>
                                </div>
                                <table class='table table-striped' align='center'>");

            // Tablo Başlıkları
            sb.Append("<tr>");
            type.GetProperties().ToList().ForEach(x =>
            {
                sb.Append($"<th>{x.Name}</th>");
            });
            sb.Append("</tr>");

            // Tablo Verileri
            _list.ForEach(x =>
            {
                var values = type.GetProperties().Select(propertyInfo => propertyInfo.GetValue(x, null)).ToList();

                sb.Append("<tr>");
                values.ForEach(value =>
                {
                    sb.Append($"<td>{value}</td>");
                });
                sb.Append("</tr>");
            });

            // HTML Kapanışı
            sb.Append("</table></body></html>");

            // PDF Dokümanı Ayarları
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                            ColorMode = ColorMode.Color,
                            Orientation = Orientation.Portrait,
                            PaperSize = PaperKind.A4,
                  },
                Objects = {
                    new ObjectSettings() {
                            PagesCount = true,
                            HtmlContent = sb.ToString(),
                            WebSettings = { DefaultEncoding = "utf-8",UserStyleSheet=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/lib/bootstrap/dist/css/bootstrap.css") },
                            HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                     }
                  }
            };

            // PDF Dönüştürücü
            var converter = _context.HttpContext!.RequestServices.GetRequiredService<IConverter>();
            return new MemoryStream(converter.Convert(doc));
        }
    }
}
