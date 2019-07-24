using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using asp_dotnet_core_angular.Extensions;
using asp_dotnet_core_angular.Model;
using Microsoft.EntityFrameworkCore;
namespace asp_dotnet_core_angular.Persistence
{
    public class VehicleRepository: IVehicleRepository
    {
        private readonly VegaDbContext context;
        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;

        }
        public void AddVehicle(Vehicle v){
            context.Vehicles.Add(v);
        }
        public void RemoveVehicle(Vehicle v){
            context.Vehicles.Remove(v);
        }
        public async Task<Vehicle> GetVehicle(int id)
        {
            return await context.Vehicles.Include(v => v.Contact)
                .Include(vm => vm.Model)
                .ThenInclude(mk => mk.Make)
                .Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
                .SingleOrDefaultAsync(i => i.Id == id);
        }

        public async Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery queryObj){
            var result = new QueryResult<Vehicle>();

             var query= context.Vehicles
                .Include(v => v.Contact)
                .Include(vm => vm.Model)
                .ThenInclude(mk => mk.Make)
                .Include(v => v.Features)
                .ThenInclude(vf => vf.Feature).AsQueryable();//.ToListAsync();
            
            if(queryObj.MakeId.HasValue)
            query = query.Where(q=>q.Model.MakeId==queryObj.MakeId.Value);

            // var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>();
            // columnsMap.Add("make", q=>q.Model.Make.Name );

            var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"]=q=>q.Model.Make.Name,
                ["model"] = q=> q.Model.Name,
                ["contact"]= q=>q.Contact.Name
            };
           

            // if(f.SortBy=="make")
            // query= ( f.isSortAscen )? query.OrderBy(s=>s.Model.MakeId):query.OrderByDescending(s=>s.Model.MakeId) ;
            
            query= query.ApplySorting(queryObj,  columnsMap);
            
            result.TotalItems = query.Count();

            query = query.ApplyPaging(queryObj); // query.Skip( (queryObj.PageNum-1) * queryObj.PageSize).Take(queryObj.PageSize);

            result.Items = await query.ToListAsync();

            return result;
        }


    }
}