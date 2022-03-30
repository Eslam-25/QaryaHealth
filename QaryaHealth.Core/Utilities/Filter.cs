using System.Collections.Generic;

namespace QaryaHealth.Core.Utilities
{
    public class Filter
    {
        public Filter()
        {
            if (Values == null)
            {
                Values = new List<int>();
            }
        }
        public string ColumnName { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }

        public bool IsMultipleValue { get; set; }
        public List<int> Values { get; set; }

    }
}