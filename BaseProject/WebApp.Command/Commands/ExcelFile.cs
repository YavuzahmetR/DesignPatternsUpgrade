using ClosedXML.Excel;
using System.Data;

namespace WebApp.Command.Commands
{
    public class ExcelFile<T>
    {
        public readonly List<T> Data;

        private readonly string _fileName = $"{typeof(T).Name}.xlsx";
        public string FileName => _fileName;
        public string FileType => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public ExcelFile(List<T> Data)
        {
            this.Data = Data;
        }

        public MemoryStream GetMemoryStream()
        {
            var wb = new XLWorkbook();
            var ds = new DataSet();
            ds.Tables.Add(GetTable());
            wb.Worksheets.Add(ds);
            var stream = new MemoryStream();
            wb.SaveAs(stream);
            return stream;
        }


        private DataTable GetTable()
        {
            var table = new DataTable();
            var type = typeof(T);
            type.GetProperties().ToList().ForEach(p => table.Columns.Add(p.Name, p.PropertyType));
            Data.ForEach(data =>
            {
                var values = type.GetProperties().Select(pInfo => pInfo.GetValue(data, null)).ToArray();
                table.Rows.Add(values);
            });
            return table;
        }
    }
}
