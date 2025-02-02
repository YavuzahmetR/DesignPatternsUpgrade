using ClosedXML.Excel;
using System.Data;

namespace WebApp.ChainOfResponsibility.ChainOfRes
{
    public class ExcelProcessHandler<T> : ProcessHandler
    {
        private DataTable GetDataTable(Object o)
        {
            var table = new DataTable();
            var type = typeof(T);

            type.GetProperties().ToList().ForEach(p=> table.Columns.Add(p.Name, p.PropertyType));

            var list = o as List<T>;

            list!.ForEach(x =>
            {
                var values = type.GetProperties().Select(p => p.GetValue(x)).ToArray();
                table.Rows.Add(values);
            });
            return table;

        }
        override public object Handle(object request)
        {
            var wb = new XLWorkbook();
            var ds = new DataSet();
            ds.Tables.Add(GetDataTable(request));
            wb.Worksheets.Add(ds);
            var excelMemoryStream = new MemoryStream();
            wb.SaveAs(excelMemoryStream);
            return base.Handle(excelMemoryStream);
        }
    }
}
