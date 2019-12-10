using System.Collections.Generic;
using System.Threading.Tasks;
using asp_dotnet_core_angular.Controllers.Resources;
using asp_dotnet_core_angular.Model;
using asp_dotnet_core_angular.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp_dotnet_core_angular.Controllers
{
    public class FeaturesController:Controller
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;

        public FeaturesController(VegaDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet("api/features")]
        public async Task<IEnumerable<KeyValuePairResource>> GetFeatures()
        {
            var resource = await context.Features.ToListAsync();
            return  mapper.Map<List<Feature>,List<KeyValuePairResource>>(resource);
        }
    }
}