using System.Linq;

namespace asp_dotnet_core_angular.Model
{
    public class PhotoSettings
    {
        public int MaxFileSize { get; set; }
        public string[] AllowedExtensions { get; set; }
        
        public bool IsSupported(string FileName)
        {
            System.Console.WriteLine(FileName);
            
            System.Console.WriteLine(AllowedExtensions.Any(s=> s== FileName.ToLower()));
            return AllowedExtensions.Any(s=> s== FileName.ToLower());  
        }
    }
}