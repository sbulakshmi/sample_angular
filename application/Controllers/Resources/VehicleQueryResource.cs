namespace asp_dotnet_core_angular.Controllers.Resources
{
    public class VehicleQueryResource
    {
       public int? MakeId { get; set; }
        public string SortBy { get; set; }
        public bool isSortAscen {get;set;}
         public int? ModelId { get; set; }
        public string Contact { get; set; }
        public int? Id { get; set; }
        public int PageSize { get; set; }
        public int PageNum { get; set; }
    }
}