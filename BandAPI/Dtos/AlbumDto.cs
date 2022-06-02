using BandAPI.Entities;
using System;

namespace BandAPI.Dtos
{
    public class AlbumDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid BandId { get; set; }
    }
}
