using asp_dotnet_core_angular.Controllers.Resources;
using asp_dotnet_core_angular.Model;
using AutoMapper;
using System.Linq;

namespace asp_dotnet_core_angular.Mapping
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<Photo, PhotoResource>();
            CreateMap( typeof(QueryResult<>), typeof(QueryResultResource<>) );
            CreateMap<Make,MakeResource>();
            CreateMap<asp_dotnet_core_angular.Model.Model,KeyValuePairResource>();
            CreateMap<Feature,KeyValuePairResource>();
            CreateMap<Vehicle,SaveVehicleResource>()
            .ForMember(v=>v.Features, opt=>opt.MapFrom(vr=> vr.Features.Select(vf=> vf.FeatureId)) );
            CreateMap<Contact,ContactResource>();
            CreateMap<Vehicle,VehicleResource>()
            .ForMember(v=> v.Features, opt=> opt.MapFrom(v=> v.Features.Select(vf=>new KeyValuePairResource{ Id = vf.Feature.Id, Name= vf.Feature.Name  })))
            .ForMember(v=> v.Make, opt=> opt.MapFrom(v=> v.Model.Make) );


            // from the API resource to domain
            CreateMap<VehicleQueryResource,VehicleQuery>();
            CreateMap<ContactResource,Contact>();
            CreateMap<SaveVehicleResource,Vehicle>()
            .ForMember(v=>v.Features, opt=> opt.Ignore())// opt.MapFrom(vr => vr.Features.Select(id=> new VehicleFeature {FeatureId = id})))
            .ForMember(v=> v.Id, opt=> opt.Ignore())
            .AfterMap((vr,v)=> {
                //to remove the resource
                var removedFeatures = v.Features.Where( f=> !vr.Features.Contains(f.FeatureId));
                foreach(var r in  removedFeatures.ToList())
                v.Features.Remove(r);
                
                //to add resource
                var addedFeatures = vr.Features.Where( id => !v.Features.Any( f=> f.FeatureId ==id )).Select(id=> new VehicleFeature {FeatureId = id});
                foreach(var a in  addedFeatures)
                v.Features.Add(a);
             });
        }
    }
}