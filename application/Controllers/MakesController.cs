using System.Collections.Generic;
using System.Threading.Tasks;
using asp_dotnet_core_angular.Controllers.Resources;
using asp_dotnet_core_angular.Model;
using asp_dotnet_core_angular.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp_dotnet_core_angular.Controllers
{
    public class MakesController : Controller
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;

        public MakesController(VegaDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
          }

        [HttpGet("api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes= await context.Makes.Include(m=> m.Models).ToListAsync();
            return mapper.Map<List<Make>, List<MakeResource>>(makes);
        }
    }
}