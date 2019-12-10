namespace asp_dotnet_core_angular.Extensions
{
    public interface IQueryObject
    {
         string SortBy { get; set; }
        bool isSortAscen {get;set;}
        int PageSize { get; set; }
        int PageNum { get; set; }

    }
}