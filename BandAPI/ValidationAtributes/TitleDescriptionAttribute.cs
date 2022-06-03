using BandAPI.Dtos;
using System.ComponentModel.DataAnnotations;

namespace BandAPI.ValidationAtributes
{
    public class TitleDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var album = (AlbumForCreatingDto)validationContext.ObjectInstance;
            if (album.Title == album.Description)
            {
                return new ValidationResult("The title and description need to be different.", new[] { "AlbumForCreatingDto" });
            }
            return ValidationResult.Success;
        }
    }
}
