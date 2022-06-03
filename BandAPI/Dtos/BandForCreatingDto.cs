using System;
using System.Collections.Generic;

namespace BandAPI.Dtos
{
    public class BandForCreatingDto
    {
        
        public string Name { get; set; }
        public DateTime Founded{ get; set; }
        public string MainGenre { get; set; }
        public List<AlbumForCreatingDto> Albums { get; set; }=new List<AlbumForCreatingDto> ();
    }
}
