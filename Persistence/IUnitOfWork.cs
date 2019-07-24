using System.Threading.Tasks;

namespace asp_dotnet_core_angular.Persistence
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}