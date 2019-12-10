using asp_dotnet_core_angular.Extensions;

namespace asp_dotnet_core_angular.Model
{
    public class VehicleQuery: IQueryObject
    {
        public int? MakeId { get; set; }
        public int? ModelId { get; set; }
        public string Contact { get; set; }
        public int? Id { get; set; }
        public string SortBy { get; set; }
        public bool isSortAscen {get;set;}

        public int PageSize { get; set; }
        public int PageNum { get; set; }
    }
}