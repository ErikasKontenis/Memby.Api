using Memby.Application.Resources;
using System.ComponentModel.DataAnnotations;

namespace Memby.Application.User
{
    public class UpsertUserDto
    {
        [Required(ErrorMessageResourceName = nameof(DataAnnotationMessages.EmailRequired), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        [EmailAddress(ErrorMessageResourceName = nameof(DataAnnotationMessages.EmailFormatIncorrect), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationMessages.PasswordRequired), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        [MinLength(6, ErrorMessageResourceName = nameof(DataAnnotationMessages.PasswordMinLengthIncorrect), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        public string Password { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationMessages.PasswordConfirmationRequired), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        [Compare(nameof(Password), ErrorMessageResourceName = nameof(DataAnnotationMessages.PasswordConfirmationDoesNotMatch), ErrorMessageResourceType = typeof(DataAnnotationMessages))]
        public string PasswordConfirmation { get; set; }
    }
}
