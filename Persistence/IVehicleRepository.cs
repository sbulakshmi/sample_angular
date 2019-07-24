using System.Collections.Generic;
using System.Threading.Tasks;
using asp_dotnet_core_angular.Model;

namespace asp_dotnet_core_angular.Persistence
{
    public interface IVehicleRepository
    {
          Task<Vehicle> GetVehicle(int id);
          void AddVehicle(Vehicle v);
         void RemoveVehicle(Vehicle v);
         Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery f);
    }
}