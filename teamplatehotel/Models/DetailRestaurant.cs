using System.Collections.Generic;
using ProjectLibrary.Database;

namespace TeamplateHotel.Models
{
    public class DetailService
    {
        public Service Service { get; set; }
        public List<Service> Services { get; set; }
        public List<ServiceGallery> ServiceGalleries { get; set; }
    }
}