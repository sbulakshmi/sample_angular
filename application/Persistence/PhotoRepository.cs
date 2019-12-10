using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_dotnet_core_angular.Model;
using Microsoft.EntityFrameworkCore;

namespace asp_dotnet_core_angular.Persistence
{
    public class PhotoRepository:IPhotoRepository
    {
        private readonly VegaDbContext context;

        public PhotoRepository(VegaDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Photo>> GetPhotos(int vehicleId){
            return await context.Photos.Where(p=> p.vehicleId == vehicleId).ToListAsync();
        }
    } 
}