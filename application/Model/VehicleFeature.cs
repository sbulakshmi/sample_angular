namespace asp_dotnet_core_angular.Model
{
    public class VehicleFeature
    {
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int FeatureId { get; set; }
        public Feature Feature { get; set; }
    }
}