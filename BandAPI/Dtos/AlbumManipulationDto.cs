using BandAPI.ValidationAtributes;
using System.ComponentModel.DataAnnotations;

namespace BandAPI.Dtos
{
    [TitleDescription(ErrorMessage = "The title and description need to be different")]
    public abstract class AlbumManipulationDto
    {
        [Required(ErrorMessage = "Title needs to be filled in")]
        [MaxLength(200, ErrorMessage = "Title needs to be up to 200 characters")]
        public string Title { get; set; }

        
        [MaxLength(400, ErrorMessage = "Description needs to be up to 400 characters")]
        public virtual string Description { get; set; }
    }
}
