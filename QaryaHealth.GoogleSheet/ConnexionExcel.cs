using LinqToExcel;

namespace QaryaHealth.GoogleSheet
{
    public class ConnexionExcel
    {
        private readonly string _pathExcelFile;
        private readonly ExcelQueryFactory _urlConnection;
        public ConnexionExcel(string path)
        {
            _pathExcelFile = path;
            _urlConnection = new ExcelQueryFactory(_pathExcelFile);
        }
        public string PathExcelFile
        {
            get
            {
                return _pathExcelFile;
            }
        }
        public ExcelQueryFactory UrlConnection
        {
            get
            {
                return _urlConnection;
            }
        }
    }
}
