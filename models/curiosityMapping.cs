using System.Collections.Generic;

namespace Curiosity.Models{
    public class CuriosityData
    {
        public int id { get; set; }
        public int sol { get; set; }
        public string cam_name { get; set; }
        public string img_src { get; set; }
        public string earth_date { get; set; }
        public string rover_name { get; set; }
        public char cat_found { get; set; }
    }
}