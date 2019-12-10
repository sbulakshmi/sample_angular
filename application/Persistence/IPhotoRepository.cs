using System.Collections.Generic;
using System.Threading.Tasks;
using asp_dotnet_core_angular.Model;

namespace asp_dotnet_core_angular.Persistence
{
    public interface IPhotoRepository
    {
       Task <IEnumerable<Photo>> GetPhotos(int vehicleId);
    }
}