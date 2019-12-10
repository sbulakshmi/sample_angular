using System.Collections.Generic;

namespace asp_dotnet_core_angular.Controllers.Resources
{
    public class QueryResultResource<T>
    {
         public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}