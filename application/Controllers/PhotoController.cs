using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using asp_dotnet_core_angular.Controllers.Resources;
using asp_dotnet_core_angular.Model;
using asp_dotnet_core_angular.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace asp_dotnet_core_angular.Controllers
{
    public class PhotoController:Controller
    {
        private readonly IVehicleRepository repository;
        private readonly IHostingEnvironment host;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IPhotoRepository photoRepository;
        private readonly PhotoSettings photoSettings;
     //   private readonly int MAX_FILE_SIZE = 1*1024*1024;
        //private readonly string[] ACCEPTED_EXTENSIONS = new string[] {".png",".jpg","jpeg"};

        //api/vehicles/{id}/upload
        public PhotoController(IVehicleRepository repository, IHostingEnvironment host, IUnitOfWork uow, IMapper mapper, IOptionsSnapshot<PhotoSettings> options, IPhotoRepository photoRepository)
        {
            this.photoSettings = options.Value;
            this.repository = repository;
            this.host = host;
            this.uow = uow;
            this.mapper = mapper;
            this.photoRepository = photoRepository;
        }
        [HttpPost("/api/vehicles/{vehicleId}/photos")]
        public async Task<IActionResult> Upload(int vehicleId, IFormFile file)
        {
            var vehicle = await repository.GetVehicle(vehicleId); //includeRelated:false
            if (vehicle == null)
                return NotFound();
            if(file==null) return BadRequest("null file");
            if(file.Length ==0 ) return BadRequest("empty file");
            if(file.Length > photoSettings.MaxFileSize ) return BadRequest("max file size exceeded");
            if( !photoSettings.IsSupported(Path.GetExtension(file.FileName) )) return BadRequest("invalid file type");


            var uploadFolderPath = Path.Combine(host.WebRootPath , "uploads");
            if(!Directory.Exists(uploadFolderPath))
            Directory.CreateDirectory(uploadFolderPath);

            var fileName= System.Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath =  Path.Combine(uploadFolderPath,fileName);

            using(var stream = new FileStream(filePath, FileMode.Create))
            {
                  await file.CopyToAsync(stream);
            }

            var photo = new Photo{FileName= fileName};
            vehicle.Photos.Add(photo);
            await uow.CompleteAsync();
            
            return Ok(mapper.Map<Photo,PhotoResource>(photo));

        }

    [HttpGet("/api/vehicles/{vehicleId}/photos")]
        public async Task<IEnumerable<PhotoResource>> GetPhotos(int vehicleId){
            var result = await photoRepository.GetPhotos(vehicleId);
            return mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(result);
        }
    }
}