using System.Collections.Generic;
using ProjectLibrary.Database;

namespace TeamplateHotel.Models
{
    public class DetailTour
    {
        public Tour Tour { get; set; }
        public List<TourGallery> TourGalleries { get; set; }
        public List<TabTour> TabTours { get; set; } 
        public List<Tour> Tours { get; set; }
    }
}