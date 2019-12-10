using asp_dotnet_core_angular.Controllers.Resources;
using asp_dotnet_core_angular.Model;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System;
using System.Threading.Tasks;

using asp_dotnet_core_angular.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace asp_dotnet_core_angular.Controllers
{
    //[Route("/api/vehicles")]   
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IVehicleRepository repository;
        private readonly IUnitOfWork uow;
        public VehiclesController(IMapper mapper, IVehicleRepository repository, IUnitOfWork uow)
        {
            this.uow = uow;
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpPost("api/vehicles/CreateVehicle")]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource)
        {
            //throw new Exception();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdated = DateTime.Now;
            repository.AddVehicle(vehicle);
            await uow.CompleteAsync();// context.SaveChangesAsync();

            vehicle = await repository.GetVehicle(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }
        [HttpPut("api/vehicles/UpdateVehicle/{id}")]
        public async Task<IActionResult> UpdateVehicle([FromBody] SaveVehicleResource vehicleResource, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await repository.GetVehicle(id);// context.Vehicles.Include(v => v.Features).SingleOrDefaultAsync(i => i.Id == id);

            mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
            vehicle.LastUpdated = DateTime.Now;
            await uow.CompleteAsync();
            vehicle = await repository.GetVehicle(id);
            //await context.SaveChangesAsync();
            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpGet("api/vehicles/GetVehicle/{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await repository.GetVehicle(id);
            if (vehicle == null)
                return NotFound();

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }
        [HttpGet("api/vehicles/GetVehicles")]
        public async Task<IActionResult> GetVehicles(VehicleQueryResource fr)
        {
            var f= mapper.Map<VehicleQueryResource, VehicleQuery>(fr);
            var vehicle = await repository.GetVehicles(f);
            if (vehicle == null)    
                return NotFound();

            var result = mapper.Map<QueryResult<Vehicle>, QueryResultResource<VehicleResource>>(vehicle);
            return Ok(result);
        }
        [HttpDelete("api/vehicles/DeleteVehicle/{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await repository.GetVehicle(id);
            if (vehicle == null)
                return NotFound();

            repository.RemoveVehicle(vehicle);
            await uow.CompleteAsync();
            return Ok(id);
        }
        //  [HttpGet("api/vehicles/CreateVehicle")]
        // public IActionResult CreateVehicle()
        // {
        //     return Ok();
        // }
    }
}