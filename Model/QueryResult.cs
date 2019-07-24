using System.Collections.Generic;

namespace asp_dotnet_core_angular.Model
{
    public class QueryResult<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}