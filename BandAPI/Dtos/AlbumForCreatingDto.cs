using BandAPI.ValidationAtributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BandAPI.Dtos
{
    [TitleDescriptionAttribute(ErrorMessage = "The title and description need to be different")]
    public class AlbumForCreatingDto : AlbumManipulationDto //:IValidatableObject
    {
       




        //[Required(ErrorMessage ="Title needs to be filled in")]
        //[MaxLength(200, ErrorMessage ="Title needs to be up to 200 characters")]
        //public string Title { get; set; }
        ////[Required]
        //[MaxLength(400, ErrorMessage = "Description needs to be up to 400 characters")]
        //public string Description { get; set; }

        ////public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        ////{
        ////    if (Title == Description)
        ////        yield return new ValidationResult("The title and description need to be different", new[] { "AlbumForCreatingDto" });
        ////}
    }
}
