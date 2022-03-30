using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using QaryaHealth.Core.Utilities;
using X.PagedList;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource;

namespace QaryaHealth.GoogleSheet.Helpers
{
    public class GoogleSheetOperationsHelper
    {
        private readonly SpreadsheetsResource.ValuesResource _googleSheetValues;
        private readonly string _spreadSheetId;
        public GoogleSheetOperationsHelper(GoogleSheetsHelper googleSheetsHelper, string spreadSheetId)
        {
            _spreadSheetId = spreadSheetId;
            _googleSheetValues = googleSheetsHelper.Service.Spreadsheets.Values;
        }

        public void Create(IList<IList<object>> item, string sheetName, string lastIndexChar)
        {
            var range = $"{sheetName}!A:{lastIndexChar}";
            var valueRange = new ValueRange
            {
                Values = item
            };
            var appendRequest = _googleSheetValues.Append(valueRange, _spreadSheetId, range);
            appendRequest.ValueInputOption = AppendRequest.ValueInputOptionEnum.USERENTERED;
            appendRequest.Execute();
        }

        public void HardDelete(int id, string sheetName, string lastIndexChar)
        {
            var range = $"{sheetName}!A{id + 1}:{lastIndexChar}{id + 1}";
            var requestBody = new ClearValuesRequest();
            var deleteRequest = _googleSheetValues.Clear(requestBody, _spreadSheetId, range);
            deleteRequest.Execute();
        }

        public void Update(int id, IList<IList<object>> item, string sheetName, string lastIndexChar)
        {
            var range = $"{sheetName}!A{id + 1}:{lastIndexChar}{id + 1}";
            var valueRange = new ValueRange
            {
                Values = item
            };
            var updateRequest = _googleSheetValues.Update(valueRange, _spreadSheetId, range);
            updateRequest.ValueInputOption = UpdateRequest.ValueInputOptionEnum.USERENTERED;
            updateRequest.Execute();
        }

        public IList<IList<object>> GetById(int id, string sheetName, string lastIndexChar)
        {
            var range = $"{sheetName}!A{id + 1}:{lastIndexChar}{id + 1}";
            var request = _googleSheetValues.Get(_spreadSheetId, range);
            return request.Execute().Values;
        }

        public IList<IList<object>> GetAll(string sheetName, string lastIndexChar)
        {
            var range = $"{sheetName}!A:{lastIndexChar}";
            var request = _googleSheetValues.Get(_spreadSheetId, range);
            return request.Execute().Values;
        }

        public IList<MatchedValueRange> GetAll(string sheetName)
        {
            List<DataFilter> dataFilters = new List<DataFilter>();
            dataFilters.Add(new DataFilter { });

            var batchFilter = new BatchGetValuesByDataFilterRequest();
            batchFilter.DataFilters = dataFilters;

            var request = _googleSheetValues.BatchGetByDataFilter(batchFilter, _spreadSheetId);
            return request.Execute().ValueRanges;
        }

        public Task<IPagedList<IList<object>>> GetPagedListAsync(int pageNumber, int pageSize, string sheetName, string lastIndexChar)
        {
            pageSize += 1;
            var range = $"{sheetName}!A:{lastIndexChar}";
            var request = _googleSheetValues.Get(_spreadSheetId, range);
            return request.Execute().Values.ToPagedListAsync(pageNumber, pageSize);
        }
    }
}