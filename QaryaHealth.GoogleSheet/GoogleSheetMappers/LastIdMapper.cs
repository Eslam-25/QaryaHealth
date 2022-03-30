using System;
using System.Collections.Generic;
using System.Linq;

namespace QaryaHealth.GoogleSheet.GoogleSheetMappers
{
    public class LastIdMapper
    {
        public static int MapFromRangeData(IList<IList<object>> values)
        {
            List<int> ids = new List<int>();
            if (values == null) return 0;

            foreach (var value in values)
            {
                if (value.Count == 0 || value[0].ToString() == "Id") continue;
                ids.Add(Convert.ToInt32(value[0]));
            }
            return ids.LastOrDefault();
        }
    }
}
