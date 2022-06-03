using BandAPI.ValidationAtributes;
using System.ComponentModel.DataAnnotations;

namespace BandAPI.Dtos
{
    [TitleDescriptionAttribute(ErrorMessage = "The title and description need to be different")]
    public class AlbumForUpdateDto : AlbumManipulationDto
    {
        //[Required(ErrorMessage = "Title needs to be filled in")]
        //[MaxLength(200, ErrorMessage = "Title needs to be up to 200 characters")]
        //public string Title { get; set; }

        //[Required]
        //[MaxLength(400, ErrorMessage = "Description needs to be up to 400 characters")]
        //public string Description { get; set; }

        [Required(ErrorMessage ="You need to fill description.")]
        public override string Description { get => base.Description; set => base.Description = value; }
    }
}
